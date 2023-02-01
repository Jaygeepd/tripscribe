import React from "react";
import { Grid, Box } from "@mui/material";
import { LeftPanel } from "./components";
import { Route, Routes, Navigate } from "react-router-dom";
import { HomePage, ProfilePage, TripViewPage } from "./pages";
import { AuthContext } from "./contexts";
import { NavRoutes } from "./constants";
import { LoginUtils } from "./utils";

// if (process.env.NODE_ENV === "development"){
//   const { worker } = require('./services/mocks/browser');
//   worker.start();
// }

const authenticatedRoutes = () => {
  return (
    <>
      <Route path={NavRoutes.Home} element={<HomePage />} />
      <Route path={NavRoutes.TripView} element={<TripViewPage />} />
      <Route path={NavRoutes.Profile} element={<ProfilePage />} />
    </>
  )
};

const unauthenticatedRoutes = () => {
  return (
    <>
      <Route path={NavRoutes.Home} element={<HomePage />} />
    </>
  )
};

function App() {

  const { state } = AuthContext.useLogin();
  const loggedIn = state.accessToken && !LoginUtils.isTokenExpired(state);

  return (
    <>
      <Grid container>
        <Grid item xs={1.5} sx={{ paddingLeft: "2vw" }}>
          <LeftPanel />
        </Grid>

        <Grid item xs={9} alignItems="center" justifyContent="center">
          <Routes>
            {!loggedIn && unauthenticatedRoutes()}
            {loggedIn && authenticatedRoutes()}
            <Route path="*" element={<Navigate to={NavRoutes.Home} />} />
          </Routes>
        </Grid>
      </Grid>
    </>
  );
}

export default App;


