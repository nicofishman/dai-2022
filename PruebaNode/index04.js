import fs from 'fs';

export const copiar = (path, newpath) => {
    try {
        const cositas = fs.readFileSync(path, 'utf8');
        if (fs.existsSync(newpath)) {
            fs.writeFileSync(newpath, cositas);
        } else {
            console.log('El archivo no existe');
        }
    } catch (error) {
        console.log(error);
    }

}