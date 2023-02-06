import { Edit, SettingsBrightnessSharp } from "@mui/icons-material";
import { Grid, IconButton, Paper, Stack, Button } from "@mui/material";
import React, { useState, useEffect } from "react";
import { useParams, Link } from "react-router-dom";
import useSWR, { Key, Fetcher } from "swr";
import { MapComponent } from "../../components";
import { Trip } from "../../types/trip";
import { StopService, TripService } from "../../services";
import { Stop } from "../../types/stop";
import { Location } from "../../types/location";
import { CreateStop, StopCard, EditTrip } from "./components";

const tempTrip: Trip = {
  id: "20",
  title: "French Trip",
  description: "Days spent in France, specifically around Paris",
  timestamp: new Date(2023, 1, 1),
  publicView: true,
};

function TripViewPage() {
  const { tripId } = useParams();

  const [createStopOpen, setCreateStopOpen] = useState(false);
  const [editTripOpen, setTripOpen] = useState(false);

  const handleEditOpen = () => {
    setTripOpen(true);
  };

  const handleEditClose = () => {
    setTripOpen(false);
  };

  const handleStopOpen = () => {
    setCreateStopOpen(true);
  };

  const handleStopClose = () => {
    setCreateStopOpen(false);
  };
  
  const [isLoading, setIsLoading] = useState(true);
  const [trip, setTrip] = useState<Trip>(tempTrip);

  const LoadInitData = async () => {
    const [foundTrip] = await Promise.all([
      TripService.getTrip(tripId as string),
    ]);

    setTrip(await foundTrip.json());
    setIsLoading(false);
  };

  useEffect(() => {
    LoadInitData();
  }, []);

  if (isLoading) return <div>Loading</div>;

  const buildStopCards = (stops: Stop[] | undefined) => {

    if (stops === undefined) return;

    return stops.map((singleStop: Stop) => (
      <Grid key={singleStop.id} item xs={12} md={6}>
        <StopCard key={singleStop.id} stop={singleStop} />
      </Grid>
    ));
  };

  return (
    <>
      <Paper
        elevation={3}
        sx={{ padding: "5vw", height: "100vh", overflow: "auto" }}
      >
        <Stack spacing={2}>
          
          <MapComponent locationList={calculateTripLocations(trip)} inputZoom={3} />

          <h1>{trip.title}</h1>
          <h3>{trip.description}</h3>
          <Button
            variant="contained"
            startIcon={<Edit />}
            sx={{ maxWidth: "20%" }}
            onClick={handleEditOpen}
          >
            Edit Trip
          </Button>

          <Grid container>{buildStopCards(trip.stops)}</Grid>
        </Stack>
        <Button variant="contained" onClick={handleStopOpen}>
          Add Stop
        </Button>
      </Paper>

      <CreateStop
        currTripId={tripId ?? "1"}
        dialogState={createStopOpen}
        setState={handleStopClose}
      />
      <EditTrip
        dialogState={editTripOpen}
        setState={handleEditClose}
        currTrip={trip}
      />
    </>
  );
}

function calculateTripLocations(searchTrip: Trip): Location[] {
  let returnList:Location[] = [];

  searchTrip.stops?.forEach((searchStop: Stop) => {
    searchStop.locations?.forEach((returnLoc: Location) => {
      returnList.push(returnLoc);
    })
  })

  return returnList;
};

export default TripViewPage;
