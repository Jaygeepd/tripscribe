import { Location } from "./location";

export type Stop = {
    stopName: string,
    dateArrived: Date, 
    dateDeparted: Date,
    stopLocations?: Location[]
};