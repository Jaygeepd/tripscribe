import { Edit } from "@mui/icons-material";
import { Grid, IconButton, Paper, Stack } from "@mui/material";
import React from "react";
import { useParams } from "react-router-dom";
import useSWR, { Key, Fetcher } from "swr";
import { Map } from "../../components";
import { Trip } from "../../types/trip";

function getTripById(url?:string) {

  const errorURL = `/trips/1`

  if(url === undefined || url === null){
    console.log(url);
    return fetch(errorURL).then((response) => response.json());
  }

  return fetch(url).then((response) => response.json());
}

function TripViewPage() {
  const { tripId } = useParams();

  const { data, error, isLoading } = useTrip(tripId);

  const currTrip: any = data

  console.log(data);

  if (isLoading) return <div>Loading</div>;
  if (error) return <div>{error.toString()}</div>;

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

function useTrip(id?: string) {

  const tripId: Key = id
  const fetcher: Fetcher<Trip, string> = (tripId) => getTripById(tripId);

  const { data, error, isLoading } = useSWR(`/trips/${id}`, fetcher);

  return {
    data,
    isLoading,
    error,
  };
}

export default TripViewPage;
