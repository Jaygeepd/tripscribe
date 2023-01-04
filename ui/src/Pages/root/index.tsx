import React, { useState } from "react";
import { ReactDOM } from "react";
import { Stack, Divider, Button, Box } from "@mui/material";
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

    return(
        <>
        <Stack 
            spacing={12} 
            direction="row"
            justifyContent="center"
            divider={<Divider orientation="vertical" flexItem />}
        >
            <Stack>
                <FlightTakeoff fontSize="large" />
                <Button variant="outlined" size="medium">Home</Button>
                <Button variant="outlined" size="medium">Trips</Button>

                <Button 
                    variant="outlined" 
                    size="medium"
                    onClick={handleLoginOpen}
                    >Log In</Button>

                <Button 
                variant="outlined" 
                size="medium"
                onClick={handleSignupOpen}
                >Sign Up</Button>
            </Stack>

            <Box>
                <h1>Content Goes Here</h1>
            </Box>

            <Box />
        </Stack>

        <Login dialogState={loginOpen} setState={handleLoginClose} />
        <SignUp dialogState={signupOpen} setState={handleSignupClose} />
        </>
    )
}

export default Layout;
