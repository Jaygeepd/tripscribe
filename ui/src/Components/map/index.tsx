import React from "react";
import GoogleMapReact from "google-map-react";
import LocationPin from "../location-pin";
import { Location } from "../../types/location";

interface IMapProps {
  location: Location;
  zoomLevel: any;
}

function Map({location, zoomLevel}: IMapProps) {
  const center = {
    lat: location.latitude,
    lng: location.longitude,
  };

  return (
    <>
      <div style={{ height: "40vh", width: "100%" }}>
        <GoogleMapReact 
        bootstrapURLKeys={{
          key: "AIzaSyCKgVAi3DvcjFOC3BqS9TEgKbRMFQq8k6I", 
          language: 'en'
       }}
        defaultCenter={center} 
        defaultZoom={zoomLevel}
        >
          <LocationPin 
            lat={location.latitude}
            lng={location.longitude}
            pinText={location.locName}
          />
        </GoogleMapReact>
      </div>
    </>
  );
}

export default Map;
