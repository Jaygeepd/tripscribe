import userEvent from "@testing-library/user-event";
import { Trip } from "../types/trip";
import { FetchUtils } from "../utils";

const updateTrip = async (updateAccount: Trip) => {
  return await FetchUtils.fetchInstance(`trips/${updateAccount.id}`, {
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

const getAllTrips =async () => {
    return await FetchUtils.fetchInstance(`trips`, {
        method: "GET",
    })
    
}

const getTripsByAccount = async (accountId: string) => {
  return (
    await FetchUtils.fetchInstance(`/accounts/${accountId}/trips`),
    {
      method: "GET",
    }
  );
};

export default {
  updateTrip,
  deleteTrip,
  getTrip,
  getAllTrips,
  getTripsByAccount
};