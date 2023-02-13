import {
  GoogleMap,
  MarkerClustererF,
  MarkerF,
} from "@react-google-maps/api";
import React from "react";
import { useState, useMemo, useRef, useCallback } from "react";
import { Location } from "../../types/location";

interface IMapProps {
  locationList?: Location[];
  inputZoom: any;
}

type LatLngLiteral = google.maps.LatLngLiteral;
type MapOptions = google.maps.MapOptions;

const containerStyle = {
  width: "74.5vw",
  height: "40vh",
};

function MapComponent({ locationList, inputZoom }: IMapProps) {
  let centerVal: LatLngLiteral = { lat: 0, lng: 0 };

  if (locationList !== undefined && locationList.length > 0) {
    centerVal = {
      lat: locationList[0].latitude,
      lng: locationList[0].longitude,
    };
    inputZoom = 8;
  }

  const [locations, setLocations] = useState<Location[]>(locationList ?? []);
  const mapRef = useRef<GoogleMap>();
  const center: LatLngLiteral = useMemo(() => centerVal, []);
  const options: MapOptions = useMemo(
    () => ({
      disableDefaultUI: false,
      clickableIcons: false,
    }),
    []
  );

  const onLoad = useCallback((map: GoogleMap) => (mapRef.current = map), []);

  return (
    <div>
      <GoogleMap
        zoom={inputZoom}
        center={center}
        mapContainerStyle={containerStyle}
        options={options}
      >
        {locations && (
          <MarkerClustererF>
            {(clusterer) => (
              <>
              {locations.map((locEntry: Location) => (
                <MarkerF
                  key={locEntry.id}
                  position={{ lat: locEntry.latitude, lng: locEntry.longitude }}
                  label={locEntry.name}
                  clusterer={clusterer}
                />
              ))}
              </>
            )
              
            }
          </MarkerClustererF>
        )}
      </GoogleMap>
    </div>
  );
}

export default MapComponent;
