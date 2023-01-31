import React from "react";
import GoogleMapReact from "google-map-react";
import LocationPin from "../location-pin";
import { Location } from "../../types/location";

interface IMapProps {
  locationList?: Location[];
  zoomLevel: any;
}

function Map({ locationList, zoomLevel }: IMapProps) {

  if (locationList === undefined) return (<></>);

  return (
    <>
      <div style={{ height: "40vh", width: "100%" }}>
        <GoogleMapReact
          bootstrapURLKeys={{
            key: "AIzaSyCKgVAi3DvcjFOC3BqS9TEgKbRMFQq8k6I",
            language: "en",
          }}
          center={{
            lat: locationList[0].geoLocation.x,
            lng: locationList[0].geoLocation.y,
          }}
          zoom={zoomLevel}
        >
        </GoogleMapReact>
      </div>
    </>
  );
}

export default Map;
