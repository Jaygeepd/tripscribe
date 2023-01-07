import React from "react";
import { Grid, Box } from "@mui/material";
import LeftPanel from "./Components/leftPanel";
import { Route, Routes } from "react-router-dom";
import { HomePage } from "./Pages"

function App() {
    return(
        <>
            <Grid container>
                <Grid item xs={1.5}>
                    <LeftPanel />
                </Grid>

                <Grid item xs={9}
                alignItems="center"
                justifyContent="center"
            >
                <Routes>
                    <Route path="/" element={<HomePage />} />
                </Routes>
            </Grid>
            </Grid>
        </>
    )
}

export default App;
