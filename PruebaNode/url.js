import { URL } from 'url';

const paramsToObject = (entries) => {
    const result = {}
    for (const [key, value] of entries) { // each 'entry' is a [key, value] tupple
        result[key] = value;
    }
    return result;
}

export const decomposeUrl = (urlString) => {
    try {
        const myURL = new URL(urlString)
        return {
            host: myURL.hostname,
            path: myURL.pathname,
            params: paramsToObject(new URLSearchParams(myURL.search).entries())
        }
    } catch (error) {
        console.log(error)
        return {
            host: '',
            path: '',
            params: {}
        }
    }
}