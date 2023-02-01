import { Divider, Paper, Stack } from "@mui/material";
import { useState, useEffect } from "react";
import { MapComponent, TripDetails } from "../../components";
import { Location } from "../../types/location";
import { Trip } from "../../types/trip";
import { LocationService, TripService } from "../../services";

const tempTrip: Trip = {
  id: "20",
  title: "French Trip",
  description: "Days spent in France, specifically around Paris",
  timestamp: new Date(2023, 1, 1),
  publicView: true,
};

function Home() {
  const [isLoading, setIsLoading] = useState(true);
  const [trips, setTrips] = useState<Trip[]>([tempTrip]);
  const [locations, setLocations] = useState<Location[]>();

  const LoadInitData = async () => {
    const [foundTrips, foundLocations] = await Promise.all([
      TripService.getAllTrips(),
      LocationService.getAllLocations(),
    ]);

    setTrips(await foundTrips.json());
    setLocations(await foundLocations.json());
    setIsLoading(false);
  };

  useEffect(() => {
    LoadInitData();
  }, []);

  if (isLoading) return <div>Loading</div>;

  const tripDisplays = trips.map((singleTrip: Trip) => (
    <TripDetails key={singleTrip.id} trip={singleTrip} />
  ));

  return (
    <>
      <Stack spacing={2} sx={{ overflow: "auto" }}>
        <MapComponent locationList={locations} inputZoom={3} />
        <Divider />
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
            <h1>Trips</h1>
            <Divider />
            {tripDisplays}
          </Stack>
        </Paper>
      </Stack>
    </>
  );
}

function getDateRange(trip: Trip) {
  let checkStops = trip.stops ?? false;

  if (!checkStops) return;

  let earliestDate = checkStops[0].dateArrived;
  let latestDate = checkStops[0].dateDeparted;

  for (const stop of checkStops) {
    if (stop.dateArrived < earliestDate) {
      earliestDate = stop.dateArrived;
    }

    if (stop.dateDeparted > latestDate) {
      latestDate = stop.dateDeparted;
    }
  }

  trip.tripStartDate = earliestDate;
  trip.tripEndDate = latestDate;
}

export default Home;
