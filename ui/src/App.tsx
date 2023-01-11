import React from "react";
import { Grid, Box } from "@mui/material";
import { LeftPanel } from "./Components";
import { Route, Routes } from "react-router-dom";
import { HomePage, TripViewPage } from "./Pages";

function App() {
  return (
    <>
      <Grid container>
        <Grid item xs={1.5} sx={{paddingLeft: "2vw"}}>
          <LeftPanel />
        </Grid>

        <Grid item xs={9} alignItems="center" justifyContent="center">
          <Routes>
            <Route path="/" element={<HomePage />} />
            <Route path="/journey-details/:{id}" element={<TripViewPage />} />
          </Routes>
        </Grid>
      </Grid>
    </>
  );
}

export default App;
