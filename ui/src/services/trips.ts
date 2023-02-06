import userEvent from "@testing-library/user-event";
import { Trip } from "../types/trip";
import { FetchUtils } from "../utils";

const createTrip = async (newTrip: Trip) => {
  const { title, description, publicView, accounts } = newTrip;
  return await FetchUtils.fetchInstance(`trips`, {
    method: "POST",
    body: JSON.stringify({
      title, 
      description, 
      publicView,
      accounts
    })
  })
}

const updateTrip = async (updateTrip: Trip) => {
  return await FetchUtils.fetchInstance(`trips/${updateTrip.id}`, {
    method: "PATCH",
  });
};

const deleteTrip = async (tripId: string) => {
  return await FetchUtils.fetchInstance(`trips/${tripId}`, {
    method: "DELETE",
  });
};

const getTrip = async (tripId: string) => {
  return await FetchUtils.fetchInstance(`trips/${tripId}`, {
    method: "GET",
  });
};

const getAllTrips = async () => {
  return await FetchUtils.fetchInstance(`trips`, {
    method: "GET",
  });
};

const getTripsByAccount = async (accountId: string) => {
  return await FetchUtils.fetchInstance(`accounts/${accountId}/trips`,
    {
      method: "GET",
    });
};

export default {
  createTrip,
  updateTrip,
  deleteTrip,
  getTrip,
  getAllTrips,
  getTripsByAccount,
};
