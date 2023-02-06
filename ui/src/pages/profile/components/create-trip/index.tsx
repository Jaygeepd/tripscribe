import { Button, Checkbox, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, FormControlLabel, FormGroup, Stack, TextField, useMediaQuery, useTheme } from '@mui/material';
import React, { useReducer } from 'react'
import { toast } from 'react-hot-toast';
import { TripService } from '../../../../services';
import { Account } from '../../../../types/account';
import { Trip } from '../../../../types/trip';

interface IHookProps {
  creatingAccount: Account,
  dialogState: boolean,
  setState: any
}

const tripReducer = (state: Trip, action: any) => {
  switch(action.type) { 
    case "Update":
      return {
        ...state,
        [action.field]: action.value,
      };
    default:
      return state;
  }
}



function CreateTrip({creatingAccount, dialogState, setState}: IHookProps) {
  const theme = useTheme();
  const fullScreen: any = useMediaQuery(theme.breakpoints.down("sm"));

  const insertAccount: Account = {
    email: creatingAccount.email,
    firstName: creatingAccount.firstName,
    lastName: creatingAccount.lastName,
    id: creatingAccount.id
  }
  
  const initialTrip: Trip = {
    title: "",
    description: "",
    publicView: false,
    accounts: [insertAccount]
  }

  const [newTrip, dispatch] = useReducer(tripReducer, initialTrip);

  const onAddTrip = async (newTrip: Trip) => {
    const response = await TripService.createTrip(newTrip);
    if (response.status === 201) {
      const newTrip = await response.json();
      setState(false);
      toast.success("New trip added!")
    } else {
      toast.error(
        "Error with creating the trip, please try again"
      )
    }
  };

  return (
    <>
    <Dialog
    fullScreen={fullScreen}
    open={dialogState}
    onClose={setState}
    >
      <DialogTitle>Add New Trip</DialogTitle>

      <DialogContent>
        <DialogContentText>
          Enter details to create a new trip
        </DialogContentText>

        <Stack spacing={2} sx={{ paddingTop: "2vh" }}>
          <TextField
            autoFocus
            margin="dense"
            fullWidth
            sx={{width: "30vw"}}
            variant="filled"
            label="Trip Title"
            value={newTrip.title}
            onChange={(e) => {
              dispatch({
                value: e.target.value,
                type: "Update",
                field: "title",
              });
            }}
          />

          <TextField
            variant="filled"
            label="Trip Description"
            value={newTrip.description}
            onChange={(e) => {
              dispatch({
                value: e.target.value,
                type: "Update",
                field: "description",
              });
            }}
            multiline
            minRows={5}
          />

          <FormGroup>
            <FormControlLabel
              control={
                <Checkbox
                  checked={newTrip.publicView}
                  onChange={(e) => {
                    dispatch({
                      value: e.target.checked,
                      type: "Update",
                      field: "publicView",
                    });
                  }}
                />
              }
              label="Public"
              labelPlacement="start"
            />
          </FormGroup>
        </Stack>
      </DialogContent>

      <DialogActions>
        <Button variant="contained" onClick={() => onAddTrip(newTrip)}>
          Create Trip
        </Button>
        <Button onClick={setState}>Cancel</Button>
      </DialogActions>

    </Dialog>
    </>
  )
}

export default CreateTrip;