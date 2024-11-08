import { scrape } from "./scraper.js";
import { insertIntoDatabase } from "./dbInserter.js";

const URL = "https://www.proshop.dk";
const ENDPOINTS = ["Gaming-baerbar", "Gaming-PC", "Spil-Gaming"];

async function main() {
    const products = await scrape(URL, ENDPOINTS);
    await insertIntoDatabase(products);
}

main();
