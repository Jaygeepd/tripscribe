import React, { useEffect, useState } from "react";
import { AccountService, TripService } from "../../services";
import { AuthContext } from "../../contexts";
import { LoginUtils } from "../../utils";
import { EditUserDetails, CreateTrip } from "./components";
import { Account } from "../../types/account";
import { Trip } from "../../types/trip";
import { Button, Divider, Grid, Paper, Stack } from "@mui/material";
import { TripDetails } from "../../components";

function ProfilePage() {
  const [isLoading, setIsLoading] = useState(true);
  const [loggedAccount, setLoggedAccount] = useState<Account>();
  const [accountTrips, setAccountTrips] = useState<Trip[]>();

  const [detailsOpen, setDetailsOpen] = useState(false);
  const [createTripOpen, setCreateTripOpen] = useState(false);

  const handleDetailsOpen = () => {
    setDetailsOpen(true);
  };

  const handleDetailsClose = () => {
    setDetailsOpen(false);
  };

  const handleCreateTripOpen = () => {
    setCreateTripOpen(true);
  };

  const handleCreateTripClose = () => {
    setCreateTripOpen(false);
  };

  const { state } = AuthContext.useLogin();
  const accountId = LoginUtils.getAccountId(state.accessToken);

  const LoadInitData = async (accountId: string) => {
    const [foundAccount, foundTrips] = await Promise.all([
      AccountService.getAccount(accountId),
      TripService.getTripsByAccount(accountId),
    ]);

    setLoggedAccount(await foundAccount.json());
    setAccountTrips(await foundTrips.json());
    setIsLoading(false);
  };

  useEffect(() => {
    LoadInitData(accountId);
  }, []);

  if (isLoading) return <div>Loading...</div>;

  const tripDisplays = accountTrips?.map((singleTrip: Trip) => (
    <TripDetails key={singleTrip.id} trip={singleTrip} />
  ));

  return (
    <>
      <Stack spacing={2}>
        <Paper sx={{ padding: "2.5vw" }}>
          <Grid container>
            <Grid item xs={12}>
              <h2>Your Details</h2>
            </Grid>
            <Grid item xs={12} sm={6}>
              <h3>{loggedAccount?.email}</h3>
            </Grid>
            <Grid item xs={12} sm={6}>
              <h3>
                {loggedAccount?.firstName} {loggedAccount?.lastName}
              </h3>
            </Grid>
            <Grid item>
              <Button onClick={handleDetailsOpen}>Edit Details</Button>
            </Grid>
            <Grid item>
              <Button onClick={handleCreateTripOpen}>Add New Trip</Button>
            </Grid>
          </Grid>
        </Paper>
        <Paper sx={{ padding: "2.5vw" }}>
          <Stack>
            <h1>Your Trips</h1>
            <Divider />
            {tripDisplays}
          </Stack>
        </Paper>
      </Stack>

      {loggedAccount && (
        <>
        <EditUserDetails
          initialAccount={loggedAccount}
          dialogState={detailsOpen}
          setState={handleDetailsClose}
        />

        <CreateTrip creatingAccount={loggedAccount} dialogState={createTripOpen} setState={handleCreateTripClose} />
        </>
      )}

      
    </>
  );
}

export default ProfilePage;
