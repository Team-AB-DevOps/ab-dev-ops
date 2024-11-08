import fs from "fs";
import { load } from "cheerio";

const URL = "https://www.proshop.dk"

const response = await fetch(URL+"/Gaming-baerbar");
const result = await response.text();
fs.writeFileSync("index.html", result);

const htmlPageString = fs.readFileSync("index.html").toString();

const $ = load(htmlPageString);

const products = []; // Array to store product information

$("#products [product]").each((index, element) => {
    const title = $(element).find(".site-product-link h2").text();
    const content = $(element).find(".site-product-link").text();
    const url = URL.concat($(element).find(".site-product-link").attr("href"));

    products.push({ title, content, url });
});

// Write the products array to a JSON file
fs.writeFileSync("products.json", JSON.stringify(products, null, 2));
