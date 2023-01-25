import React, { useState } from "react";
import { ReactDOM } from "react";
import { Link } from "react-router-dom";
import { Stack, Button, Avatar } from "@mui/material";
import { FlightTakeoff } from "@mui/icons-material";
import { Login, SignUp } from "../index";
import { AuthContext } from "../../contexts";
import { LoginUtils } from "../../utils";
import { UserDisplay } from "./components";

function LeftPanel() {
  const [loginOpen, setLoginOpen] = useState(false);
  const [signupOpen, setSignupOpen] = useState(false);
  const { state, dispatch } = AuthContext.useLogin();

  const loggedIn = state.accessToken && !LoginUtils.isTokenExpired(state);

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
    localStorage.clear();
    dispatch({ type: "logout" });
  };

  return (
    <>
      <Stack
        alignItems="center"
        justifyContent="center"
        sx={{ position: "fixed" }}
      >
        <FlightTakeoff
          sx={{ paddingTop: "10vh", paddingBottom: "4vh" }}
          fontSize="large"
        />

        <Button
          sx={navButtonStyle}
          variant="outlined"
          size="medium"
          component={Link}
          to={`/`}
        >
          Home
        </Button>
        <Button sx={navButtonStyle} variant="outlined" size="medium">
          Trips
        </Button>

        {!loggedIn && (
          <Button
            variant="outlined"
            size="medium"
            sx={navButtonStyle}
            onClick={handleLoginOpen}
          >
            Log In
          </Button>
        )}

        {!loggedIn && (
          <Button
            variant="outlined"
            size="medium"
            sx={navButtonStyle}
            onClick={handleSignupOpen}
          >
            Sign Up
          </Button>
        )}

        {loggedIn && (
          <>
            <Button
              variant="outlined"
              size="medium"
              sx={navButtonStyle}
              onClick={handleClickLogOut}
            >
              Log Out
            </Button>
            <Avatar>JD</Avatar>
            <h2>Jane Doe</h2>
          </>
        )}

        {loggedIn && <UserDisplay />}
      </Stack>

      <Login dialogState={loginOpen} setState={handleLoginClose} />
      <SignUp dialogState={signupOpen} setState={handleSignupClose} />
    </>
  );
}

export default LeftPanel;
