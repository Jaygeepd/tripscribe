import React from "react";
import { Grid, Box } from "@mui/material";
import LeftPanel from "./Components/leftPanel";

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
                <Box>
                    <h1>Content Goes Here</h1>
                </Box>
            </Grid>
            </Grid>
        </>
    )
}

export default App;
