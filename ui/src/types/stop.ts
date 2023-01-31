import { Location } from "./location";

export type Stop = {
    id?: string,
    name: string,
    dateArrived: Date, 
    dateDeparted: Date,
    tripId: string,
    locations?: Location[]
};