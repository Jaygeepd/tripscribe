import React from "react";
import { Grid, Box } from "@mui/material";
import { LeftPanel } from "./components";
import { Route, Routes } from "react-router-dom";
import { HomePage, TripViewPage } from "./pages";

function App() {
  return (
    <>
      <Grid container>
        <Grid item xs={1.5} sx={{ paddingLeft: "2vw" }}>
          <LeftPanel />
        </Grid>

        <Grid item xs={9} alignItems="center" justifyContent="center">
          <Routes>
            <Route path="/" element={<HomePage />} />
            <Route path="/trips" element={<TripViewPage />}>
              <Route path=":tripid" element={<TripViewPage />} />
            </Route>
          </Routes>
        </Grid>
      </Grid>
    </>
  );
}

export default App;
