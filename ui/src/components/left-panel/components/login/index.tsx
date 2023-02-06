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
  InputAdornment,
  IconButton,
} from "@mui/material";
import { AuthenticationService, StorageService } from "../../../../services";
import { StorageTypes } from "../../../../constants";
import { AuthContext } from "../../../../contexts";
import { useNavigate } from "react-router-dom";
import toast from "react-hot-toast";
import { Visibility, VisibilityOff } from "@mui/icons-material";

interface IHookProps {
  dialogState: boolean;
  setState: any;
}

function Login(props: IHookProps) {
  const theme = useTheme();
  const fullScreen: any = useMediaQuery(theme.breakpoints.down("sm"));

  const [showPassword, setShowPassword] = useState(false);

  const handleClickShowPassword = () => setShowPassword((show) => !show);
  const handleMouseDownPassword = () => setShowPassword((show) => !show);

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const { dispatch } = AuthContext.useLogin();

  const authentication = async () => {
    const response = await AuthenticationService.authenticate(email, password);
    if (response.status === 200) {
      const loginResult = await response.json();
      StorageService.setLocalStorage(loginResult, StorageTypes.AUTH);
      StorageService.setLocalStorage(email, StorageTypes.EMAIL);
      dispatch({
        type: "authentication",
        ...loginResult,
      });
      toast.success("Successfully Logged In");
      props.setState(false);
    } else {
      toast.error("Invalid authentication details");
    }
  };

  return (
    <>
      <Dialog
        fullScreen={fullScreen}
        open={props.dialogState}
        onClose={props.setState}
      >
        <DialogTitle>Login</DialogTitle>

        <DialogContent>
          <DialogContentText>
            Enter your account details below to access your account
          </DialogContentText>

          <TextField
            autoFocus
            margin="dense"
            id="emailField"
            label="Email Address"
            type="email"
            fullWidth
            variant="filled"
            onChange={(_) => setEmail(_.target.value)}
          />

          <TextField
            margin="dense"
            id="passwordField"
            label="Password"
            type={showPassword ? "text" : "password"}
            fullWidth
            variant="filled"
            onChange={(_) => setPassword(_.target.value)}
            InputProps={{
              endAdornment: (
                <InputAdornment position="end">
                  <IconButton
                    aria-label="toggle password visibility"
                    onClick={handleClickShowPassword}
                    onMouseDown={handleMouseDownPassword}
                  >
                    {showPassword ? <Visibility /> : <VisibilityOff />}
                  </IconButton>
                </InputAdornment>
              ),
            }}
          />
        </DialogContent>

        <DialogActions>
          <Button
            variant="contained"
            onClick={() => authentication()}
            onKeyDown={(_) => {
              if (_.key === "Enter") {
                authentication();
              }
            }}
          >
            Login
          </Button>
          <Button onClick={props.setState}>Back</Button>
        </DialogActions>
      </Dialog>
    </>
  );
}

export default Login;
