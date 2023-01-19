import React, { useState } from "react";
import { ReactDOM } from "react";
import { useMediaQuery, useTheme, Dialog, DialogTitle, DialogContent, DialogContentText, TextField, DialogActions, Button } from "@mui/material";
import { AuthenticationService, StorageService } from "../../services";
import { StorageTypes } from "../../constants";
import { AuthContext } from "../../contexts";
import { useNavigate } from "react-router-dom";
import toast from "react-hot-toast";

interface hookProps {
    dialogState: boolean;
    setState: any;
}

function Login(props: hookProps){
    const theme = useTheme();
    const fullScreen:any = useMediaQuery(theme.breakpoints.down('sm'));

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
            })
            props.setState;
            ;
        } else {
            toast.error("Invalid authentication details");
        }
    }
    
    return(
        <>
        <Dialog
            fullScreen={fullScreen}
            open={props.dialogState}
            onClose={props.setState}
        >

        <DialogTitle>
            Login
        </DialogTitle>

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
                variant="standard"
                onChange={(_) => setEmail(_.target.value)}
            />

            <TextField 
                autoFocus
                margin="dense"
                id="passwordField"
                label="Password"
                type="password"
                fullWidth
                variant="standard"
                onChange={(_) => setPassword(_.target.value)}
            />
        </DialogContent>

        <DialogActions>
            <Button variant="contained" onClick={() => authentication()}>Login</Button>
            <Button onClick={props.setState}>Back</Button>
        </DialogActions>
        </Dialog>
        </>
    )
}

export default Login;
