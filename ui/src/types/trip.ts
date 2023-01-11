import { Stop } from "./stop";

export type Trip = {
    title: string,
    tripStartDate?: Date,
    tripEndDate?: Date, 
    tripDesc: string,
    tripTimestamp: Date,
    tripStops?: Stop[]
};