import { Stop } from "../types/stop";
import { FetchUtils } from "../utils";

const createStop = async (newStop: Stop) => {
  const { stopName, dateArrived, dateDeparted, tripId } = newStop;
  return await FetchUtils.fetchInstance(`accounts`, {
    method: "POST",
    body: JSON.stringify({
      stopName,
      dateArrived,
      dateDeparted,
      tripId,
    }),
  });
};

const updateStop = async (updateStop: Stop) => {
  return await FetchUtils.fetchInstance(`stops/${updateStop.id}`, {
    method: "PATCH",
  });
};

const deleteStop = async (stopId: string) => {
  return await FetchUtils.fetchInstance(`stops/${stopId}`, {
    method: "DELETE",
  });
};

const getStop = async (stopId: string) => {
  return await FetchUtils.fetchInstance(`stops/${stopId}`, {
    method: "GET",
  });
};

const getAllStops = async () => {
  return await FetchUtils.fetchInstance(`stops`, {
    method: "GET",
  });
};

const getStopsByTripId = async (tripId: string) => {
  return await FetchUtils.fetchInstance(`trips/${tripId}/stops`, {
    method: "GET",
  });
};

export default {
  createStop,
  updateStop,
  deleteStop,
  getStop,
  getAllStops,
  getStopsByTripId,
};
