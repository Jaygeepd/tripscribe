import { Location } from "./location";

export type Stop = {
    id?: string,
    stopName: string,
    dateArrived: Date, 
    dateDeparted: Date,
    tripId: string,
    stopLocations?: Location[]
};