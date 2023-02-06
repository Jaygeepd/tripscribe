import React, { useReducer, useState } from "react";
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
  Stack,
  IconButton,
  InputAdornment,
} from "@mui/material";
import { AccountService } from "../../../../services";
import { Account } from "../../../../types/account";
import { toast } from "react-hot-toast";
import { Visibility, VisibilityOff } from "@mui/icons-material";

interface IHookProps {
  dialogState: boolean;
  setState: any;
}

interface NewAccount extends Account {
  confirmPassword: string;
}

const initialAccount: NewAccount = {
  email: "",
  firstName: "",
  lastName: "",
  password: "",
  confirmPassword: "",
};

const accountReducer = (state: NewAccount, action: any) => {
  switch (action.type) {
    case "Update":
      return {
        ...state,
        [action.field]: action.value,
      };
    default:
      return state;
  }
};

function SignUp(props: IHookProps) {
  const [newAccount, dispatch] = useReducer(accountReducer, initialAccount);
  const theme = useTheme();
  const fullScreen: any = useMediaQuery(theme.breakpoints.down("sm"));

  const [showPassword, setShowPassword] = useState(false);
  const [showConfirmPassword, setShowConfirmPassword] = useState(false);

  const handleClickShowPassword = (clickPassword: boolean) => {
    clickPassword
      ? setShowPassword((show) => !show)
      : setShowConfirmPassword((show) => !show);
  };

  const handleMouseDownPassword = (clickPassword: boolean) => {
    clickPassword
      ? setShowPassword((show) => !show)
      : setShowConfirmPassword((show) => !show);
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

          <Stack spacing={2} sx={{ paddingTop: "2vh" }}>
            <TextField
              autoFocus
              variant="filled"
              label="Email Address"
              onChange={(e) =>
                dispatch({
                  value: e.target.value,
                  type: "Update",
                  field: "email",
                })
              }
              value={newAccount.email}
            />

            <TextField
              label="First Name"
              variant="filled"
              onChange={(e) =>
                dispatch({
                  value: e.target.value,
                  type: "Update",
                  field: "firstName",
                })
              }
              value={newAccount.firstName}
            />

            <TextField
              label="Last Name"
              variant="filled"
              onChange={(e) =>
                dispatch({
                  value: e.target.value,
                  type: "Update",
                  field: "lastName",
                })
              }
              value={newAccount.lastName}
            />

            <TextField
              label="Password"
              variant="filled"
              type={showPassword ? "text" : "password"}
              onChange={(e) =>
                dispatch({
                  value: e.target.value,
                  type: "Update",
                  field: "password",
                })
              }
              value={newAccount.password}
              InputProps={{
                endAdornment: (
                  <InputAdornment position="end">
                    <IconButton
                      aria-label="toggle password visibility"
                      onClick={(e) => handleClickShowPassword(true)}
                      onMouseDown={(e) => handleMouseDownPassword(true)}
                    >
                      {showPassword ? <Visibility /> : <VisibilityOff />}
                    </IconButton>
                  </InputAdornment>
                ),
              }}
            />

            <TextField
              label="Confirm Password"
              variant="filled"
              type={showConfirmPassword ? "text" : "password"}
              onChange={(e) =>
                dispatch({
                  value: e.target.value,
                  type: "Update",
                  field: "confirmPassword",
                })
              }
              value={newAccount.confirmPassword}
              InputProps={{
                endAdornment: (
                  <InputAdornment position="end">
                    <IconButton
                      aria-label="toggle password visibility"
                      onClick={(e) => handleClickShowPassword(false)}
                      onMouseDown={(e) => handleMouseDownPassword(false)}
                    >
                      {showConfirmPassword ? <Visibility /> : <VisibilityOff />}
                    </IconButton>
                  </InputAdornment>
                ),
              }}
            />
          </Stack>
        </DialogContent>

        <DialogActions>
          <Button variant="contained" onClick={() => onAddAccount(newAccount)}>
            Sign Up
          </Button>
          <Button onClick={props.setState}>Back</Button>
        </DialogActions>
      </Dialog>
    </>
  );
}

export default SignUp;
