import React from "react";
import GoogleMapReact from "google-map-react";
import LocationPin from "./locationPin";
import { Location } from "../types/location";

interface IMapProps {
  location: Location;
  zoomLevel: any;
}

function Map(props: IMapProps) {
  const center = {
    lat: props.location.latitude,
    lng: props.location.longitude,
  };

  return (
    <>
      <GoogleMapReact defaultCenter={center} defaultZoom={props.zoomLevel}>
        <LocationPin pinText={props.location.address} />
      </GoogleMapReact>
    </>
  );
}

export default Map;
