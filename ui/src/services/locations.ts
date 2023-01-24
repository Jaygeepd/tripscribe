import userEvent from "@testing-library/user-event";
import { Location } from "../types/location";
import { FetchUtils } from "../utils";

const createLocation = async (newLocation: Location) => {
  const { locName, latitude, longitude, dateVisited, locationType, stopId } =
    newLocation;
  return await FetchUtils.fetchInstance(`locations`, {
    method: "POST",
    body: JSON.stringify({
      locName,
      latitude,
      longitude,
      dateVisited,
      locationType,
      stopId,
    }),
  });
};

const updateLocation = async (updateLocation: Location) => {
  return await FetchUtils.fetchInstance(`locations/${updateLocation.id}`, {
    method: "PATCH",
  });
};

const deleteLocation = async (locationId: string) => {
  return await FetchUtils.fetchInstance(`locations/${locationId}`, {
    method: "DELETE",
  });
};

const getLocation = async (locationId: string) => {
  return await FetchUtils.fetchInstance(`locations/${locationId}`, {
    method: "GET",
  });
};

const getLocationsByStop = async (stopId: string) => {
  return (
    await FetchUtils.fetchInstance(`/stops/${stopId}/locations`),
    {
      method: "GET",
    }
  );
};

export default {
  createLocation,
  updateLocation,
  deleteLocation,
  getLocation,
  getLocationsByStop,
};
