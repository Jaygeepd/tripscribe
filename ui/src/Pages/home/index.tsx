import { Divider, Paper, Stack } from "@mui/material";
import React from "react";
import { ReactDOM } from "react";
import { Map, TripDetails} from "../../Components";
import { useLoadScript } from "@react-google-maps/api";
import { Location } from "../../types/location";
import { Trip } from "../../types/trip";
import { Stop } from "../../types/stop";

function Home() {
  const tempLoc: Location = {
    locName: "Eiffel Tower",
    latitude: 48.8584,
    longitude: 2.2945,
    dateVisited: new Date(2022, 2, 2),
    locationType: "National Monument"
  };

  const tempStop: Stop = {
    stopName: "Paris",
    dateArrived: new Date(2022, 2, 1),
    dateDeparted: new Date(2022, 2, 3),
    stopLocations: [tempLoc]
  }

  const exampleFirstTrip: Trip = {
    title: "French Trip",
    tripStartDate: new Date(2022, 2, 1),
    tripEndDate: new Date(2022, 2, 5),
    tripDesc: "Explorations around France, especially Paris",
    tripTimestamp: new Date,
    tripStops: [tempStop]
  };

  const exampleSecondTrip: Trip = {
    title: "Canadian Adventure",
    tripStartDate: new Date(2022, 6, 22),
    tripEndDate: new Date(2022,7, 10),
    tripDesc: "Journey into the Great White North",
    tripTimestamp: new Date,
  };

  const trips: Trip[] = [
    exampleFirstTrip, exampleSecondTrip
  ]

  const tripDisplays = trips.map((singleTrip) => <TripDetails key={singleTrip.title} trip={singleTrip} />)

  return (
    <>
      <Paper sx={{ padding: "5vw", height: "79vh", overflow: "auto" }}>
        <Stack>
          <Map location={tempLoc} zoomLevel={13}/>
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
