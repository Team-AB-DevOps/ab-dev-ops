import mysql from "mysql2/promise";
import "dotenv/config";

const dbConfig = {
    host: process.env.MYSQL_HOST,
    user: process.env.MYSQL_USER,
    database: process.env.MYSQL_DATABASE,
    password: process.env.MYSQL_PASSWORD,
    port: process.env.MYSQL_PORT || 3306,
    multipleStatements: true,
};

const connection = await mysql.createConnection(dbConfig);

export { connection };
