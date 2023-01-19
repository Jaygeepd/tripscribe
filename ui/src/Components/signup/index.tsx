import React, { useState } from "react";
import { ReactDOM } from "react";
import {
  useMediaQuery,
  useTheme,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogContentText,
  TextField,
  DialogActions,
  Button,
} from "@mui/material";
import { AccountService } from "../../services";
import { Account } from "../../types/account";
import { toast } from "react-hot-toast";

interface hookProps {
  dialogState: boolean;
  setState: any;
}

function SignUp(props: hookProps) {
  const theme = useTheme();
  const fullScreen: any = useMediaQuery(theme.breakpoints.down("sm"));

  const [email, setEmail] = useState("");
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [password, setPassword] = useState("");
  const [errorState, setErrorState] = useState({
    firstName: false,
    lastName: false,
    password: false,
    email: false,
  });

  const addAccountClick = () => {
    const formErrors = {
      firstName: firstName === "",
      lastName: lastName === "",
      email: email === "",
      password: password === "",
    };

    setErrorState(formErrors);

    if (
      !formErrors.firstName &&
      !formErrors.lastName &&
      !formErrors.email &&
      !formErrors.password
    ) {
        const newAccount:Account = {
            id: "",
            email: email,
            firstName: firstName, 
            lastName: lastName,
            password: password,
        };

        onAddAccount(newAccount);
        setFirstName("");
            setLastName("");
            setEmail("");
            setErrorState({
                firstName: false,
                lastName: false,
                email: false,
                password: false,
            })
    }
  };

  const onAddAccount = async (newAccount: Account) => {
    const response = await AccountService.createAccount(newAccount);
    if (response.status === 201) {
      const newAccount = await response.json();
      props.setState(false);
      toast.success("New acccount created successfully!");
    } else {
      toast.error(
        "There was a problem with account creation, please try again"
      );
    }
  };

  return (
    <>
      <Dialog
        fullScreen={fullScreen}
        open={props.dialogState}
        onClose={props.setState}
      >
        <DialogTitle>Sign Up</DialogTitle>

        <DialogContent>
          <DialogContentText>
            Enter details below to create your Tripscribe account
          </DialogContentText>

          <TextField
            autoFocus
            value={email}
            required
            helperText={errorState.email && "Email is required"}
            error={errorState.email}
            label="Email Address"
            fullWidth
            onChange={(_) => setEmail(_.target.value)}
          />

          <TextField
            value={firstName}
            required
            helperText={errorState.firstName && "First Name is required"}
            error={errorState.firstName}
            label="First Name"
            fullWidth
            onChange={(_) => setFirstName(_.target.value)}
          />

          <TextField
            value={lastName}
            required
            helperText={errorState.lastName && "Last Name is required"}
            error={errorState.lastName}
            label="Last Name"
            fullWidth
            onChange={(_) => setLastName(_.target.value)}
          />

          <TextField
            value={password}
            required
            helperText={errorState.password && "Password is required"}
            error={errorState.password}
            label="Password"
            fullWidth
            onChange={(_) => setPassword(_.target.value)}
          />


        </DialogContent>

        <DialogActions>
          <Button variant="contained" onClick={addAccountClick}>
            Sign Up
          </Button>
          <Button onClick={props.setState}>Back</Button>
        </DialogActions>
      </Dialog>
    </>
  );
}

export default SignUp;
