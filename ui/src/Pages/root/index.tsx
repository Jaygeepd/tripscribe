import React, { useState } from "react";
import { ReactDOM } from "react";
import { Stack, Divider, Button, Box, Grid } from "@mui/material";
import { FlightTakeoff } from "@mui/icons-material";
import Login from "../../Components/login";
import SignUp from "../../Components/signup";

function Layout() {

    const [loginOpen, setLoginOpen] = useState(false);
    const [signupOpen, setSignupOpen] = useState(false);

    const handleLoginOpen = () => {
        setLoginOpen(true);
    };

    const handleLoginClose = () => {
        setLoginOpen(false);
    };

    const handleSignupOpen = () => {
        setSignupOpen(true);
    };

    const handleSignupClose = () => {
        setSignupOpen(false);
    };

    const navButtonStyle = {
        width: 120,
        marginBottom: 1
    } as const;

    return(
        <>
        <Grid container
            spacing={5}
            direction="row"
            justifyContent="center"
        >
            <Grid item xs ={2}>
                <Stack
                    alignItems="center"
                    justifyContent="center"
                >
                    <FlightTakeoff 
                        sx={{"paddingTop": "10vh", "paddingBottom": "4vh"}} 
                        fontSize="large" />

                    <Button sx={navButtonStyle} variant="outlined" size="medium">Home</Button>
                    <Button sx={navButtonStyle} variant="outlined" size="medium">Trips</Button>

                    <Button 
                        variant="outlined" 
                        size="medium"
                        sx={navButtonStyle}
                        onClick={handleLoginOpen}
                        >Log In</Button>

                    <Button 
                        variant="outlined" 
                        size="medium"
                        sx={navButtonStyle}
                        onClick={handleSignupOpen}
                        >Sign Up</Button>
                </Stack>
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

        <Login dialogState={loginOpen} setState={handleLoginClose} />
        <SignUp dialogState={signupOpen} setState={handleSignupClose} />
        </>
    )
}

export default Layout;
