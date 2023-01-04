import React from "react";
import { ReactDOM } from "react";
import { useMediaQuery, useTheme, Dialog, DialogTitle, DialogContent, DialogContentText, TextField, DialogActions, Button } from "@mui/material";

interface hookProps {
    dialogState: boolean;
    setState: any;
}

function SignUp(props: hookProps){
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
            Sign Up
        </DialogTitle>

        <DialogContent>
            <DialogContentText>
                Enter details below to create your Tripscribe account
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
                id="firstNameField"
                label="First Name"
                type="text"
                fullWidth
                variant="standard"
            />

            <TextField 
                autoFocus
                margin="dense"
                id="lastNameField"
                label="Last Name"
                type="text"
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

            <TextField 
                autoFocus
                margin="dense"
                id="passwordConfirmField"
                label="Confirm Password"
                type="password"
                fullWidth
                variant="standard"
            />

        </DialogContent>

        <DialogActions>
            <Button variant="contained" onClick={props.setState}>Sign Up</Button>
            <Button onClick={props.setState}>Back</Button>
        </DialogActions>
        </Dialog>
        </>
    )
}

export default SignUp;
