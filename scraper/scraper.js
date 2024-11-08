import fs from "fs";
import { load } from "cheerio";
import { connection } from "./database.js";

const URL = "https://www.proshop.dk";

const response = await fetch(URL + "/Gaming-baerbar");
const result = await response.text();
fs.writeFileSync("index.html", result);

const htmlPageString = fs.readFileSync("index.html").toString();

const $ = load(htmlPageString);

const products = []; // Array to store product information

$("#products [product]").each((index, element) => {
    const title = $(element).find(".site-product-link h2").text();
    const content = $(element).find(".site-product-link").text();
    const url = URL.concat($(element).find(".site-product-link").attr("href"));
    const language = "dk";

    products.push({ title, content, url, language });
});

// Write the products array to a JSON file
fs.writeFileSync("products.json", JSON.stringify(products, null, 2));

// Read and parse the JSON file
const productsData = JSON.parse(fs.readFileSync("products.json", "utf-8"));

// Prepare an array to hold the values for each product
const values = productsData.map((product) => [product.title, product.content, product.url, product.language]);

// Define the SQL query for a bulk insert
const query = `
    INSERT INTO pages (title, content, url, language)
    VALUES ?
    ON DUPLICATE KEY UPDATE
        title = VALUES(title),
        content = VALUES(content),
        language = VALUES(language)
`;

try {
    // Perform the bulk insert
    await connection.query(query, [values]);
    console.log("All products have been successfully inserted.");
} catch (error) {
    console.error("Error during bulk insert:", error);
} finally {
    // Close the database connection
    await connection.end();
}
