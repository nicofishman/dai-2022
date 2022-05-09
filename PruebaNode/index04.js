import fs from 'fs';

export const cambiarAdentroDelArchivo = (newcontent, path) => {
    fs.writeFileSync(path, newcontent);
}