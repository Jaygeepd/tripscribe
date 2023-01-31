export type Location = {
    id?: string,
    name: string,
    geoLocation: {
        isEmpty: boolean,
        x: number, 
        y: number,
    },
    dateVisited: Date, 
    locationType: string,
    stopId: string
};