import { Divider, Paper, Stack } from "@mui/material";
import React from "react";
import { ReactDOM } from "react";
import Map from "../../Components/map";
import { Location } from "../../types/location";

function Home() {

    const tempLoc: Location = {
        address: "Champ de Mars, 5 Av. Anatole France, 75007 Paris, France",
        latitude: 48.8584,
        longitude: 2.2945
    };

    return(
        <>
        <Paper>
            <Stack>
                <Map location={tempLoc} zoomLevel={11}/>
                <h1>Journeys</h1>
                <Divider />
            </Stack>
        </Paper>
        </>
    )
};

export default Home;