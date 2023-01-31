import {
  useMediaQuery,
  useTheme,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogContentText,
  TextField,
  Stack,
  DialogActions,
  Button,
  Checkbox,
  FormGroup,
  FormControlLabel,
} from "@mui/material";
import React, { useReducer } from "react";
import { toast } from "react-hot-toast";
import { TripService } from "../../../../services";
import { Trip } from "../../../../types/trip";

interface IHookProps {
  dialogState: boolean;
  setState: any;
  currTrip: Trip;
}

const tripReducer = (state: Trip, action: any) => {
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

function EditTrip(props: IHookProps) {
  const initialTrip = props.currTrip;
  const [updateTrip, dispatch] = useReducer(tripReducer, initialTrip);
  const theme = useTheme();
  const fullScreen: any = useMediaQuery(theme.breakpoints.down("sm"));

  const onUpdateTrip = async (newTrip: Trip) => {
    const response = await TripService.updateTrip(newTrip);
    if (response.status === 204) {
      toast.success("Trip Details Updated!");
      props.setState(false);
    } else {
      toast.error("An error occurred with the update, please try again");
    }
  };

  return (
    <>
      <Dialog
        fullScreen={fullScreen}
        open={props.dialogState}
        onClose={props.setState}
        
      >
        <DialogTitle>Edit Trip</DialogTitle>

        <DialogContent>
          <DialogContentText>Update the trip details below</DialogContentText>
        

        <Stack spacing={2} sx={{ paddingTop: "2vh" }}>
          <TextField
            autoFocus
            margin="dense"
            fullWidth
            sx={{width: "30vw"}}
            variant="filled"
            label="Trip Title"
            value={updateTrip.title}
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
            value={updateTrip.description}
            onChange={(e) => {
              dispatch({
                value: e.target.value,
                type: "Update",
                field: "tripDesc",
              });
            }}
            multiline
            minRows={5}
          />

          <FormGroup>
            <FormControlLabel
              control={
                <Checkbox
                  checked={updateTrip.publicView}
                  onChange={(e) => {
                    dispatch({
                      value: e.target.checked,
                      type: "Update",
                      field: "public",
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
          <Button variant="contained" onClick={() => onUpdateTrip(updateTrip)}>
            Save Details
          </Button>
        </DialogActions>
      </Dialog>
    </>
  );
}

export default EditTrip;
