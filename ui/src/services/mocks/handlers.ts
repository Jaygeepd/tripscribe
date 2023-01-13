import { NestCamWiredStandTwoTone } from "@mui/icons-material";
import { rest } from "msw";

const trips = {
    1 : {title: "French Trip", 
    description: "Days spent in France, specifically around Paris",
    timestamp: new Date(2023, 1, 1),
    public: true},
    2 : {title: "Canadian Adventure", 
    description: "Journeys in the great white north",
    timestamp: new Date(2023, 1, 6),
    public: true, StartDate: new Date(2022, 6, 22), EndDate: new Date(2022, 7, 10),
    stops: []},
    3 : {title: "Scandanavian Sojourn", 
    description: "Sweden and Norway stay",
    timestamp: new Date(2022, 12, 15),
    public: false}
}

export const handlers = [
    rest.get("/trips", async (req, res, ctx) => {
        return res(
            ctx.status(200),
            ctx.delay(500),
            ctx.json(
                Object.entries(trips).map(([id, data]) => {
                    return { id, ...data };
                })
            )
        )
    })
]