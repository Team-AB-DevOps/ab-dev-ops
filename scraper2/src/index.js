const mysql = require("mysql2/promise");
require("dotenv/config");
const fs = require("fs");
const fetch = require("node-fetch");
const { load } = require("cheerio");

module.exports = async function (context, myTimer) {
    const URL = "https://www.proshop.dk";
    const ENDPOINTS = ["Gaming-baerbar", "Gaming-PC", "Spil-Gaming"];
    //

    const products = [];

    context.log("Timer function processed request.");

    const dbConfig = {
        host: process.env.MYSQL_HOST,
        user: process.env.MYSQL_USER,
        database: process.env.MYSQL_DATABASE,
        password: process.env.MYSQL_PASSWORD,
        port: process.env.MYSQL_PORT || 3306,
    };

    const knex = require("knex")({
        client: "mysql2",
        connection: dbConfig,
    });

    for (const endpoint of ENDPOINTS) {
        let page = 1;
        let tempTitle = "";

        while (true) {
            let breakloop = false;
            const response = await fetch(`${URL}/${endpoint}?pn=${page}`, {
                headers: {
                    "User-Agent":
                        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.82 Safari/537.36",
                },
            });

            if (!response.ok) {
                context.log(`Failed to fetch ${endpoint}, page ${page}:`, response.statusText);
                break;
            }

            const result = await response.text();

            context.log(`Fetched page content for ${endpoint}, page ${page}:`, result);

            const $ = load(result);

            if ($("#products [product]").length === 0) {
                break;
            }

            $("#products [product]").each((index, element) => {
                const title = $(element).find(".site-product-link h2").text();
                const content = $(element).find(".site-product-link").text();
                const url = URL.concat($(element).find(".site-product-link").attr("href"));
                const language = "en";

                if (tempTitle === title) {
                    breakloop = true;
                    return false;
                }

                if (index === 0) {
                    tempTitle = title;
                }

                products.push({ title, content, url, language });
            });

            if (breakloop) {
                break;
            }

            page++;
        }
    }

    const insertIntoDatabase = async () => {
        try {
            await knex("pages").insert(products).onConflict("title").merge(); // Updates the rows if they already exist
            context.log("All products have been successfully inserted or updated.");
        } catch (error) {
            context.log("Error during bulk insert:", error);
        } finally {
            await knex.destroy(); // Close the connection
        }
    };

    try {
        context.log(products);
        await insertIntoDatabase();
        context.log("Timer function processed successfully.");
    } catch (error) {
        context.log("An error occurred:", error);
    }
};
