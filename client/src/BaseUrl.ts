const isProduction = import.meta.env.PROD;

const prod = "https://server-blue-paper-9306.fly.dev";
const dev = "http://localhost:5173";
export const finalUrl = isProduction ? prod : dev;