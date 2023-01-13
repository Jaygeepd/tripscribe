import { Button, Paper, Stack } from "@mui/material";
import React, { useMemo } from "react";
import { Trip } from "../../types/trip";
import { format as dateFormat } from "date-fns";

export interface ITripProps {
  trip: Trip;
}

const dateRange = (startDate?: Date, endDate?: Date): string => {
  
  if (startDate == null || endDate == null){ 
    return "No Date Range Found"
  }
  
  return dateFormat(startDate, "dd/MM/yyyy")
    .concat(" - ")
    .concat(dateFormat(endDate, "dd/MM/yyyy"));

}

function TripDetails({trip}: ITripProps) {

  const dateRangeString = useMemo<string>(() => dateRange(trip.tripStartDate, trip.tripEndDate),
   [trip.tripStartDate, trip.tripEndDate])

  return (
    <Paper elevation={12} sx={{ padding: "2vh", marginBottom: "3vh" }}>
      <Stack>
        <h1>{trip.title}</h1>
        <h4>{dateRangeString}</h4>
        <p>{trip.tripDesc}</p>
        <Button sx={{ marginLeft: "45vw", width: "15vw" }} variant="contained">
          Journey Details
        </Button>
      </Stack>
    </Paper>
  );
}

export default TripDetails;
