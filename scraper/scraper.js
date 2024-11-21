import fs from "fs";
import { load } from "cheerio";

async function scrape(scrapeUrl, endpoints) {
    const products = [];
    for (const endpoint of endpoints) {
        let page = 1;
        let tempTitle = "";

        while (true) {
            let breakloop = false;
            const response = await fetch(scrapeUrl + `/${endpoint}?pn=${page}`);
            const result = await response.text();

            const $ = load(result);

            if ($("#products [product]").length === 0) {
                break;
            }

            $("#products [product]").each((index, element) => {
                const title = $(element).find(".site-product-link h2").text();
                const content = $(element).find(".site-product-link").text();
                const url = scrapeUrl.concat($(element).find(".site-product-link").attr("href"));
                const language = "dk";

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
    return products;
}

export { scrape };
