import {
  Paper,
  Stack,
  Table,
  TableContainer,
  TableHead,
  TableRow,
  TableCell,
  TableBody,
  Button,
  Grid,
} from "@mui/material";
import React, { useMemo } from "react";
import { format as dateFormat } from "date-fns";
import { Stop } from "../../../../types/stop";
import { Location } from "../../../../types/location";
import { ConsoleWriter } from "istanbul-lib-report";
import { ConfirmationNumberOutlined } from "@mui/icons-material";
import { Link } from "react-router-dom";

interface IStopProps {
  stop: Stop;
}

const dateRange = (startDate?: Date, endDate?: Date) => {
  if (startDate == null || endDate == null) {
    return "No Date Range Found";
  }

  return dateFormat(startDate, "dd/MM/yyyy")
    .concat(" - ")
    .concat(dateFormat(endDate, "dd/MM/yyyy"));
};

function StopCard({ stop }: IStopProps) {
  const dateRangeString = useMemo<string>(
    () => dateRange(stop.dateArrived, stop.dateDeparted),
    [stop.dateArrived, stop.dateDeparted]
  );

  return (
    <>
      <Paper elevation={5} sx={{ paddingLeft: "2vw", margin: "1.5vw" }}>
        <Stack>
          <h2>{stop.stopName}</h2>
          <h4>{dateRangeString}</h4>
          <h3>Places Visited</h3>

          <TableContainer>
            <Table>
              <TableHead>
                <TableRow>
                  <TableCell>Name</TableCell>
                  <TableCell>Date Visited</TableCell>
                  <TableCell>Details</TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {stop.stopLocations?.map((stopLoc: Location) => (
                  <TableRow key={stopLoc.id}>
                    <TableCell>{stopLoc.locName}</TableCell>
                    <TableCell>
                      {dateFormat(stopLoc.dateVisited, "dd/MM/yyyy")}
                    </TableCell>
                    <TableCell>
                      <Button
                        variant="contained"
                        component={Link}
                        to={`/locations/${stopLoc.id}`}
                      >
                        View Details
                      </Button>
                    </TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
          </TableContainer>
          <Grid
            container
            alignItems={"center"}
            justifyContent={"center"}
            sx={{ paddingTop: "2vh", paddingBottom: "2vh", paddingLeft:"3vw"}}
          >
            <Grid item xs={12} md={6}>
              <Button variant="contained" component={Link} to={`/stop-view/${stop.id}`}>View Stop</Button>
            </Grid>
            <Grid item xs={12} md={6}>
              <Button variant="contained" component={Link} to={`/create-location/${stop.id}`}>Add Location</Button>
            </Grid>
          </Grid>
        </Stack>
      </Paper>
    </>
  );
}

export default StopCard;
