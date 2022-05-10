import { getAllInfoByISO } from "iso-country-currency";

export const getCurrencyByISO = (iso) => {
    const currency = getAllInfoByISO(iso);
    return currency.currency;
}