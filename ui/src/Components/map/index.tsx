import React from "react";
import GoogleMapReact from "google-map-react";
import LocationPin from "../location-pin";
import { Location } from "../../types/location";

interface IMapProps {
  locationList: Location[];
  zoomLevel: any;
}

function Map({ locationList, zoomLevel }: IMapProps) {
  return (
    <>
      <div style={{ height: "40vh", width: "100%" }}>
        <GoogleMapReact
          bootstrapURLKeys={{
            key: "AIzaSyCKgVAi3DvcjFOC3BqS9TEgKbRMFQq8k6I",
            language: "en",
          }}
          center={{
            lat: locationList[0].latitude,
            lng: locationList[0].longitude,
          }}
          zoom={zoomLevel}
        >
          {/* {
            (locationList.map((singlePin: Location) => (
              <LocationPin
                key={singlePin.id}
                lat={singlePin.latitude}
                lng={singlePin.latitude}
                pinText={singlePin.locName}
              />
            )))
          } */}

          <LocationPin
            lat={locationList[0].latitude}
            lng={locationList[0].latitude}
            pinText={locationList[0].locName}
          />

          <LocationPin
            lat={locationList[1].latitude}
            lng={locationList[1].latitude}
            pinText={locationList[1].locName}
          />
        </GoogleMapReact>
      </div>
    </>
  );
}

export default Map;
