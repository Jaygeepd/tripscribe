import { Divider, Paper, Stack } from "@mui/material";
import React, { useState, useEffect } from "react";
import useSWR from "swr";
import { ReactDOM } from "react";
import { Map, TripDetails } from "../../components";
import { Location } from "../../types/location";
import { Trip } from "../../types/trip";
import { Stop } from "../../types/stop";
import { TripService } from "../../services";

const tempTrip: Trip = {
  id: "20",
  title: "French Trip",
  tripDesc: "Days spent in France, specifically around Paris",
  tripTimestamp: new Date(2023, 1, 1),
  public: true,
};

const tempStop: Stop = {
  id: "20",
  stopName: "Paris",
  dateArrived: new Date(2022, 1, 2),
  dateDeparted: new Date(2022, 1, 4),
  tripId: "20",
};

const tempLoc: Location = {
  id: "20",
  locName: "Eiffel Tower",
  latitude: 48.8584,
  longitude: 2.2945,
  dateVisited: new Date(2022, 2, 2),
  locationType: "Tourist Spot",
  stopId: "20",
};

function Home() {
  const [isLoading, setIsLoading] = useState(true);
  const [trips, setTrips] = useState([tempTrip]);

  useEffect(() => {
    TripService.getAllTrips().then(async (response) => {
      const foundTrips = await response.json();
      setTrips(foundTrips);
      setIsLoading(false);
    });
  });

  if (isLoading) return <div>Loading</div>;

  const tripDisplays = trips.map((singleTrip: Trip) => (
    <TripDetails key={singleTrip.id} trip={singleTrip} />
  ));

  return (
    <>
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
          <Map location={tempLoc} zoomLevel={13} />
          <Divider />
          <h1>Trips</h1>
          <Divider />
          {tripDisplays}
        </Stack>
      </Paper>
    </>
  );
}

function getDateRange(trip: Trip) {
  
}

export default Home;
