import {LibraryClient} from "./generated-ts-client.ts";

const isProduction = import.meta.env.PROD;

const prod = "https://server-blue-paper-9306.fly.dev";
const dev = "http://localhost:5009";


export const finalUrl = isProduction ? prod : dev;

export const libraryApi = new LibraryClient(finalUrl)
