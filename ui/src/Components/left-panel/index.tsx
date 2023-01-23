import React, { useState } from "react";
import { ReactDOM } from "react";
import { Link } from "react-router-dom";
import { Stack, Button, } from "@mui/material";
import { FlightTakeoff } from "@mui/icons-material";
import { Login, SignUp } from "../index";

interface panelProps{
  userLoggedIn?: boolean | string
}

function LeftPanel({userLoggedIn}: panelProps) {
  const [loginOpen, setLoginOpen] = useState(false);
  const [signupOpen, setSignupOpen] = useState(false);

  if(typeof(userLoggedIn) == "string"){
    userLoggedIn = false;
  };

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
    marginBottom: 1,
  } as const;

  const handleClickLogOut = () => {
    setSignupOpen(true);
  }

  return (
    <>
      <Stack alignItems="center" justifyContent="center" sx={{position: "fixed"}}>
        <FlightTakeoff
          sx={{ paddingTop: "10vh", paddingBottom: "4vh" }}
          fontSize="large"
        />

        <Button sx={navButtonStyle} variant="outlined" size="medium"
        component={ Link } to={`/`}
        >
          Home
        </Button>
        <Button sx={navButtonStyle} variant="outlined" size="medium">
          Trips
        </Button>

        {!userLoggedIn && 
        <Button
          variant="outlined"
          size="medium"
          sx={navButtonStyle}
          onClick={handleLoginOpen}
        >
          Log In
        </Button>
        }

        {!userLoggedIn && 
        <Button
          variant="outlined"
          size="medium"
          sx={navButtonStyle}
          onClick={handleSignupOpen}
        >
          Sign Up
        </Button>
        }

        {userLoggedIn && 
        <Button
          variant="outlined"
          size="medium"
          sx={navButtonStyle}
          onClick={handleClickLogOut}
        > 
          Log Out
        </Button>
        }
      </Stack>

      <Login dialogState={loginOpen} setState={handleLoginClose} />
      <SignUp dialogState={signupOpen} setState={handleSignupClose} />
    </>
  );
}

export default LeftPanel;
