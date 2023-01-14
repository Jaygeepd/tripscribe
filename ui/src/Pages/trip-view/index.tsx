import { Edit } from "@mui/icons-material";
import { Grid, IconButton, Paper, Stack } from "@mui/material";
import React from "react";
import { useParams } from "react-router-dom";
import useSWR from "swr";
import { Map } from "../../components";
import { Trip } from "../../types/trip";

function getTripById() {
  return fetch("/trips/${id}").then((response) => response.json());
}

function TripViewPage() {
  const { tripId } = useParams();

  const { data, error, isLoading } = useSWR(["trips", tripId], getTripById);

  const currTrip:Trip = data

  console.log(data);

  if (isLoading) return (<div>Loading</div>)

  return (
    <>
      <Paper
        elevation={3}
        sx={{ padding: "5vw", height: "100vh", overflow: "auto" }}
      >
        <Grid container>
          <Grid item xs={4}>
            <h1>{currTrip.title}</h1>
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
