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
  Box,
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
  const [newGeoLoc, setNewGeoLoc] = useState<LatLngLiteral>();
  const [newDateVisited, setDateVisited] = useState<Date | null>(
    dateFns.date(new Date())
  );

  const handleInput = (e: any) => {
    setGeoValue(e.target.value)
  };

  const handleSelect =
    ({ description }: any) =>
    () => {
      setGeoValue(description, false);
      clearSuggestions();

      getGeocode({ address: description }).then((results) => {
        const { lat, lng } = getLatLng(results[0]);
        setNewGeoLoc({lat: lat, lng: lng})
      });
    };

    const renderSuggestions = () =>
    data.map((suggestion) => {
      const {
        place_id,
        structured_formatting: { main_text, secondary_text },
      } = suggestion;

      return (
        <li key={place_id} onClick={handleSelect(suggestion)}>
          <strong>{main_text}</strong> <small>{secondary_text}</small>
        </li>
      );
    });

  const createLocation = async () => {
    const newLocation: Location = {
      name: newLocName,
      locationType: newLocType,
      dateVisited: newDateVisited as Date,
      latitude: newGeoLoc?.lat as number,
      longitude: newGeoLoc?.lng as number,
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
              <Box>
              <input 
                value = {geoValue}
                onChange = {handleInput}
                placeholder = "Location Address"
              />
              {status === "OK" && <ul>{renderSuggestions()}</ul>}
              </Box>
              

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
            Create Location
          </Button>
          <Button onClick={setState}>Back</Button>
        </DialogActions>
      </Dialog>
    </LocalizationProvider>
  );
}

export default AddLocation;
