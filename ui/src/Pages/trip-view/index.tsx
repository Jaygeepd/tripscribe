import { Edit } from "@mui/icons-material";
import { Grid, IconButton, Paper, Stack } from "@mui/material";
import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import useSWR, { Key, Fetcher } from "swr";
import { Map } from "../../components";
import { Trip } from "../../types/trip";
import { TripService } from "../../services";

const tempTrip: Trip = {
  id: 20,
  title: "French Trip", 
  tripDesc: "Days spent in France, specifically around Paris",
  tripTimestamp: new Date(2023, 1, 1),
  public: true
}

function TripViewPage() {

  debugger;
  const { tripId } = useParams();
  
  console.log(tripId);

  const [isLoading, setIsLoading] = useState(true);
  const [ trip, setTrip ] = useState(tempTrip);
  
  TripService.getTrip(tripId as string).then(async (response) => {
    const foundTrip = await response.json();
    setTrip(foundTrip);
    setIsLoading(false);
  });

  if (isLoading) return <div>Loading</div>;

  return (
    <>
      <Paper
        elevation={3}
        sx={{ padding: "5vw", height: "100vh", overflow: "auto" }}
      >
        <Grid container>
          <Grid item xs={4}>
            <h1>{tripId}</h1>
            <h1>{trip.title}</h1>
          </Grid>
          <Grid item xs={8} justifyContent="flex-end">
            <IconButton>
              <Edit />
              <h4>Edit Trip</h4>
            </IconButton>
          </Grid>
        </Grid>
      </Paper>
    </>
  );
}
export default TripViewPage;