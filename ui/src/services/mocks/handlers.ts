import { rest } from "msw";

const trips = {
    "1" : {title: "French Trip", 
    description: "Days spent in France, specifically around Paris",
    timestamp: new Date(2023, 1, 1),
    public: true},
    "2" : {title: "Canadian Adventure", 
    description: "Journeys in the great white north",
    timestamp: new Date(2023, 1, 6),
    public: true, StartDate: new Date(2022, 6, 22), EndDate: new Date(2022, 7, 10),
    stops: []},
    "3" : {title: "Scandanavian Sojourn", 
    description: "Sweden and Norway stay",
    timestamp: new Date(2022, 12, 15),
    public: false}
};

export const handlers = [
    // Get all Trips
    rest.get("/trips", async (req, res, ctx) => {
        return res(
            ctx.status(200),
            ctx.json(
                Object.entries(trips).map(([id, data]) => {
                    return { id, ...data };
                })
            )
        )
    }),

    // Get trip by id
    rest.get("/trips/:tripid", (req, res, ctx) => {

        type ObjectKey = keyof typeof trips;

        const tripId = (<ObjectKey>req.params.tripid);

        const data = trips[tripId];

        if (typeof data === "undefined"){
            return res(ctx.status(404))
        }

        return res(
            ctx.json({tripId, ...data})
        )
    })
]