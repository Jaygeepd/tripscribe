import {
  useTheme,
  useMediaQuery,
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
  Stack,
  TextField,
} from "@mui/material";
import DateFnsAdapter from "@date-io/date-fns";
import React, { useState } from "react";
import { Location } from "../../types/location";
import { LocationService } from "../../services";
import { toast } from "react-hot-toast";
import { AdapterDateFns } from "@mui/x-date-pickers/AdapterDateFns";
import { DesktopDatePicker } from "@mui/x-date-pickers/DesktopDatePicker";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import usePlacesAutocomplete, {
  getGeocode,
  getLatLng,
} from "use-places-autocomplete";
import createStop from "../../pages/trip-view/components/create-stop";

interface IHookProps {
  currStopId: string;
  dialogState: boolean;
  setState: any;
}

type LatLngLiteral = google.maps.LatLngLiteral;

function AddLocation({ currStopId, dialogState, setState }: IHookProps) {
  const theme = useTheme();
  const fullScreen: any = useMediaQuery(theme.breakpoints.down("sm"));

  const dateFns = new DateFnsAdapter();
  const {
    ready,
    value: geoValue,
    setValue: setGeoValue,
    suggestions: { status, data },
    clearSuggestions,
  } = usePlacesAutocomplete();

  const [newLocName, setNewLocName] = useState("");
  const [newLocType, setNewLocType] = useState("Attraction");
  const [newLat, setNewLat] = useState(0 as number);
  const [newLng, setNewLng] = useState(0 as number);
  const [newDateVisited, setDateVisited] = useState<Date | null>(
    dateFns.date(new Date())
  );

  const createLocation = async () => {
    const newLocation: Location = {
      name: newLocName,
      locationType: newLocType,
      dateVisited: newDateVisited as Date,
      latitude: newLat,
      longitude: newLng,
      stopId: currStopId,
    };

    const response = await LocationService.createLocation(newLocation);
    if (response.status === 201) {
      toast.success("New Location Added!");
      setState(false);
      window.location.reload();
    } else {
      toast.error("Error creating your location");
    }
  };

  return (
    <LocalizationProvider dateAdapter={AdapterDateFns}>
      <Dialog fullScreen={fullScreen} open={dialogState} onClose={setState}>
        <DialogTitle>Add New Location</DialogTitle>

        <DialogContent>
          <>
            <DialogContentText sx={{ paddingTop: "2vh", paddingBottom: "2vh" }}>
              Enter the details of the location below
            </DialogContentText>

            <Stack spacing={2}>
              <TextField
                autoFocus
                margin="dense"
                id="nameField"
                label="Location Name"
                type="text"
                fullWidth
                variant="filled"
                onChange={(_) => setNewLocName(_.target.value)}
              />

              <DesktopDatePicker
                label="Date Visited"
                inputFormat="dd/MM/yyyy"
                value={newDateVisited}
                onChange={(newValue: Date | null) => setDateVisited(newValue)}
                renderInput={(params) => <TextField {...params} />}
              />

              {/* <TextField
                margin="dense"
                id="geoLocField"
                label="Enter Address of Location"
                type="text"
                fullWidth
                variant="filled"
                value={geoValue}
                onChange={(_) => setGeoValue(_.target.value)}
              /> */}

              {/* <TextField
              
                margin="dense"
                id="latField"
                label="Latitude"
                type="text"
                fullWidth
                variant="filled"
                onChange={(_) => setNewLat(_.target.value)}
              />

              <TextField
                
                margin="dense"
                id="lngField"
                label="Longitude"
                type="text"
                fullWidth
                variant="filled"
                onChange={(_) => setNewLng(_.target.value)}
              /> */}
            </Stack>
          </>
        </DialogContent>
        <DialogActions>
          <Button
            variant="contained"
            onClick={() => createLocation()}
            onKeyDown={(_) => {
              if (_.key === "Enter") {
                createLocation();
              }
            }}
          >
            Create Stop
          </Button>
          <Button onClick={setState}>Back</Button>
        </DialogActions>
      </Dialog>
    </LocalizationProvider>
  );
}

export default AddLocation;
