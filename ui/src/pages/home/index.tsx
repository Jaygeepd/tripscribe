import { Divider, Paper, Stack } from "@mui/material";
import { useState, useEffect } from "react";
import { MapComponent, TripDetails } from "../../components";
import { Location } from "../../types/location";
import { Trip } from "../../types/trip";
import { Stop } from "../../types/stop";
import { TripService } from "../../services";
import React from "react";

function Home() {
  const [isLoading, setIsLoading] = useState(true);
  const [trips, setTrips] = useState<Trip[]>();

  const LoadInitData = async () => {
    const [foundTrips] = await Promise.all([
      TripService.getAllTrips()
    ]);

    setTrips(await foundTrips.json());
    setIsLoading(false);
  };

  useEffect(() => {
    LoadInitData();
  }, []);

  if (isLoading) return <div>Loading</div>;

  const tripDisplays = trips?.map((singleTrip: Trip) => (
    <TripDetails key={singleTrip.id} trip={singleTrip} />
  ));

  return (
    <>
      <Stack spacing={2} sx={{ overflow: "auto" }}>
        <MapComponent locationList={getAttachedLocations(trips)} inputZoom={6} />
        <Divider />
        <Paper
          sx={{
            paddingLeft: "2.5vw",
            paddingRight: "2.5vw",
            paddingTop: "2.5vw",
            height: "92vh",
            overflow: "auto",
          }}
        >
          <Stack>
            <h1>Trips</h1>
            <Divider />
            {tripDisplays}
          </Stack>
        </Paper>
      </Stack>
    </>
  );
}

function getAttachedLocations(searchTrips?: Trip[]): Location[] {

  let returnList:Location[] = [];

  if(searchTrips === undefined) return returnList;

  searchTrips.forEach((singleTrip: Trip) => {
    singleTrip.stops?.forEach((searchStop: Stop) => {
      searchStop.locations?.forEach((setLocation: Location) => {
        returnList.push(setLocation)
      })
    })
  });

  return returnList;
}

function getDateRange(trip: Trip) {
  let checkStops = trip.stops ?? false;

  if (!checkStops) return;

  let earliestDate = checkStops[0].dateArrived;
  let latestDate = checkStops[0].dateDeparted;

  for (const stop of checkStops) {
    if (stop.dateArrived < earliestDate) {
      earliestDate = stop.dateArrived;
    }

    if (stop.dateDeparted > latestDate) {
      latestDate = stop.dateDeparted;
    }
  }

  trip.tripStartDate = earliestDate;
  trip.tripEndDate = latestDate;
}

export default Home;
