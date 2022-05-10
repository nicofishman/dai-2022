import { URL } from 'url';

const paramsToObject = (entries) => {
    const result = {}
    for (const [key, value] of entries) { // each 'entry' is a [key, value] tupple
        result[key] = value;
    }
    return result;
}

export const decomposeUrl = (urlString) => {
    const myURL = new URL(urlString)
    return {
        host: myURL.hostname,
        path: myURL.pathname,
        params: paramsToObject(new URLSearchParams(myURL.search).entries())
    }
}