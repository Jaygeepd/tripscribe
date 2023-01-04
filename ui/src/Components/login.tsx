import React from "react";
import { ReactDOM } from "react";
import { useMediaQuery, useTheme, Dialog, DialogTitle, DialogContent, DialogContentText, TextField, DialogActions, Button } from "@mui/material";

interface hookProps {
    dialogState: boolean;
    setState: any;
}

function Login(props: hookProps){
    const theme = useTheme();
    const fullScreen:any = useMediaQuery(theme.breakpoints.down('md'));

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
            />

            <TextField 
                autoFocus
                margin="dense"
                id="passwordField"
                label="Password"
                type="password"
                fullWidth
                variant="standard"
            />
        </DialogContent>

        <DialogActions>
            <Button variant="contained" onClick={props.setState}>Login</Button>
            <Button onClick={props.setState}>Back</Button>
        </DialogActions>
        </Dialog>
        </>
    )
}

export default Login;
