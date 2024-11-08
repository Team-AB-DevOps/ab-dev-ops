import { scrape } from "./scraper.js";
import { insertIntoDatabase } from "./dbInserter.js";
const { app } = require("@azure/functions");

const URL = "https://www.proshop.dk";
const ENDPOINTS = ["Gaming-baerbar", "Gaming-PC"];

//, "Spil-Gaming"

async function main() {
    const products = await scrape(URL, ENDPOINTS);
    await insertIntoDatabase(products);
}

app.timer("timerTrigger1", {
    schedule: "*/2 * * * *",
    handler: async (myTimer, context) => {
        context.log("Timer function processed request.");
        await main();
    },
});

