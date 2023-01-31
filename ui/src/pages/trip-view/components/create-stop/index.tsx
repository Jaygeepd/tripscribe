import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
  TextField,
  useMediaQuery,
  useTheme,
} from "@mui/material";
import React, { useState } from "react";
import { AdapterDateFns } from "@mui/x-date-pickers/AdapterDateFns";
import DateFnsAdapter from "@date-io/date-fns";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { DesktopDatePicker } from "@mui/x-date-pickers/DesktopDatePicker";
import { StopService } from "../../../../services";
import { Stop } from "../../../../types/stop";
import { toast } from "react-hot-toast";

interface IHookProps {
  currTripId: string;
  dialogState: boolean;
  setState: any;
}

function CreateStop(props: IHookProps) {
  const theme = useTheme();
  const fullScreen: any = useMediaQuery(theme.breakpoints.down("sm"));

  const dateFns = new DateFnsAdapter();

  const [newStopName, setNewStopName] = useState("");
  const [newDateArrived, setDateArrived] = useState<Date | null>(
    dateFns.date(new Date())
  );
  const [newDateDeparted, setDateDeparted] = useState<Date | null>(
    dateFns.date(new Date())
  );

  const handleDateChange = (newValue: Date | null) => {
    if (newValue === undefined) {
      let dateValue: Date = dateFns.date(new Date());
    } else {
      let dateValue: Date = newValue as Date;
    }
  };

  const createStop = async () => {
    const newStop: Stop = {
      name: newStopName,
      dateArrived: newDateArrived as Date,
      dateDeparted: newDateDeparted as Date,
      tripId: props.currTripId,
    };

    const response = await StopService.createStop(newStop);
    if (response.status === 204) {
      toast.success("New Stop added! Returning to the Trip");
      props.setState(false);
    } else {
      toast.error("Error creating your stop");
    }
  };

  return (
    <LocalizationProvider dateAdapter={AdapterDateFns}>
      <Dialog
        fullScreen={fullScreen}
        open={props.dialogState}
        onClose={props.setState}
      >
        <DialogTitle>Add New Stop</DialogTitle>

        <DialogContent>
          <>
            <DialogContentText>
              Enter the details of the stop below
            </DialogContentText>

            <TextField
              autoFocus
              margin="dense"
              id="nameField"
              label="Name"
              type="text"
              fullWidth
              variant="filled"
              onChange={(_) => setNewStopName(_.target.value)}
            />

            <DesktopDatePicker
              label="Date Arrived"
              inputFormat="dd/MM/YYYY"
              value={newDateArrived}
              onChange={(newValue: Date | null) => setDateArrived(newValue)}
              renderInput={(params) => <TextField {...params} />}
            />

            <DesktopDatePicker
              label="Date Departed"
              inputFormat="dd/MM/YYYY"
              value={newDateDeparted}
              onChange={(newValue: Date | null) => setDateDeparted(newValue)}
              renderInput={(params) => <TextField {...params} />}
            />
          </>
        </DialogContent>
        <DialogActions>
          <Button
            variant="contained"
            onClick={() => createStop()}
            onKeyDown={(_) => {
              if (_.key === "Enter") {
                createStop();
              }
            }}
          >
            Create Stop
          </Button>
          <Button onClick={props.setState}>Back</Button>
        </DialogActions>
      </Dialog>
    </LocalizationProvider>
  );
}

export default CreateStop;
