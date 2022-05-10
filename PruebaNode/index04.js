import fs from 'fs';

export const cambiarAdentroDelArchivo = (newcontent, path) => {
    try {
        fs.writeFileSync(path, newcontent);
    } catch (error) {
        console.log(error);
    }

}