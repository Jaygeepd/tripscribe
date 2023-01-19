import { Trip } from "./trip";

export type Account = {
    id: string,
    email: string, 
    firstName: string,
    lastName: string, 
    password: string,
    trips?: Trip[]
};