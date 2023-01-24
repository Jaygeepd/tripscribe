import { Edit } from "@mui/icons-material";
import { Grid, IconButton, Paper, Stack, Button } from "@mui/material";
import React, { useState, useEffect } from "react";
import { useParams, Link } from "react-router-dom";
import useSWR, { Key, Fetcher } from "swr";
import { Map } from "../../components";
import { Trip } from "../../types/trip";
import { StopService, TripService } from "../../services";
import { Stop } from "../../types/stop";
import { Location } from "../../types/location";
import { StopCard } from "./components";

const tempLoc: Location = {
  id: "20",
  locName: "Eiffel Tower",
  latitude: 48.8584,
  longitude: 2.2945,
  dateVisited: new Date(2022, 1, 1),
  locationType: "Tourist Spot",
  stopId: "20",
};

const tempStop: Stop = {
  id: "20",
  stopName: "Paris",
  dateArrived: new Date(2022, 1, 1),
  dateDeparted: new Date(2022, 1, 4),
  tripId: "20",
  stopLocations: [tempLoc],
};

const tempStop2: Stop = {
  id: "21",
  stopName: "Nice",
  dateArrived: new Date(2022, 1, 4),
  dateDeparted: new Date(2022, 1, 6),
  tripId: "20",
  stopLocations: [tempLoc],
};

const tempTrip: Trip = {
  id: "20",
  title: "French Trip",
  tripDesc: "Days spent in France, specifically around Paris",
  tripTimestamp: new Date(2023, 1, 1),
  public: true,
  tripStops: [tempStop, tempStop2],
};

function TripViewPage() {
  const { tripId } = useParams();

  console.log(tripId);

  const [isLoading, setIsLoading] = useState(true);
  const [trip, setTrip] = useState(tempTrip);
  const [stops, setStops] = useState([tempStop, tempStop2]);

  useEffect(() => {
    TripService.getTrip(tripId as string).then(async (response) => {
      const foundTrip = await response.json();
      setTrip(foundTrip);
      setIsLoading(false);
    });
  });

  // useEffect(() => {
  //   StopService.getStopsByTripId(tripId as string).then(async (response) => {
  //     const foundStops = await response.json();
  //     setStops(foundStops);
  //   });
  // });

  

  if (isLoading) return <div>Loading</div>;
  
  const stopCards = stops.map((singleStop: Stop) => (
    
    <Grid key={singleStop.id} item xs={12} md={6}>
    <StopCard key={singleStop.id} stop={singleStop} />
    </Grid>
  ));

  return (
    <>
      <Paper
        elevation={3}
        sx={{ padding: "5vw", height: "100vh", overflow: "auto" }}
      >
        <Stack spacing={2}>
          {/* <Map locationList={tempStop.stopLocations ?? [tempLoc]} zoomLevel={13} /> */}

          <h1>{trip.title}</h1>
          <Button
            variant="contained"
            startIcon={<Edit />}
            sx={{ maxWidth: "20%" }}
          >
            Edit Trip
          </Button>

          <Grid container>
              {stopCards}
          </Grid>
        </Stack>
        <Button variant="contained" component={Link} to={`/create-stop/${tripId}`}>Add Stop</Button>
      </Paper>
    </>
  );
}
export default TripViewPage;
