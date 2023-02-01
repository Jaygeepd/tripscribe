import React, { useEffect, useState } from "react";
import { Location } from "../../../../types/location";
interface IMarkerProps {
  markedLoc?: Location;
}

function MapMarker({ markedLoc }: IMarkerProps) {
  const [marker, setMarker] = useState<IMarkerProps>();

  return <></>;
}

export default MapMarker;
