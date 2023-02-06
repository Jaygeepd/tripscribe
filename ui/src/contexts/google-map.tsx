import React, { createContext, useState } from "react";
import { AuthContext } from ".";
import { StorageTypes } from "../constants";
import { StorageService } from "../services";

interface IGoogleMapProps { 
    isLoaded: boolean
}

const MapContext = createContext<IGoogleMapProps | undefined>(undefined);

function MapProvider({children}: any) {
    const map = StorageService.getLocalStorage(StorageTypes.MAP) ?? {};
    const [ state, setState ] = useState(map);
    const value = state;

    return <MapContext.Provider value={value}>{children}</MapContext.Provider>
};

export default MapContext;