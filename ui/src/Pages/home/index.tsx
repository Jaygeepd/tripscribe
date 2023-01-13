import { Divider, Paper, Stack } from "@mui/material";
import React from "react";
import useSWR from "swr";
import { ReactDOM } from "react";
import { Map, TripDetails } from "../../components";
import { Location } from "../../types/location";
import { Trip } from "../../types/trip";
import { Stop } from "../../types/stop";

function getTrips(){
  return fetch("/trips").then((response) => response.json())
}

const tempLoc: Location = {
  locName: "Eiffel Tower",
  latitude: 48.8584,
  longitude: 2.2945,
  dateVisited: new Date(2022, 2, 2),
  locationType: "Tourist Spot"
};

function Home() {
  
  const { data, error } = useSWR("trips", getTrips);

  const tripDisplays = data.map((singleTrip:Trip) => (
    <TripDetails key={singleTrip.title} trip={singleTrip} />
  ));

  return (
    <>
      <Paper sx={{ padding: "5vw", height: "79vh", overflow: "auto" }}>
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

export default Home;
