import { Stop } from "./stop";

export type Trip = {
    id: number,
    title: string,
    tripDesc: string,
    tripTimestamp: Date,
    public: boolean,
    tripStartDate?: Date,
    tripEndDate?: Date, 
    tripStops?: Stop[]
};