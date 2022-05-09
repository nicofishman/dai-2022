import { sumar, restar, dividir, multiplicar, potencia, raiz, pi } from './src/modules/matematica.js';
import Alumno from './index03.js';
import { cambiarAdentroDelArchivo } from './index04.js';

const nombreYApellido = 'Juan Perez';
console.log("Hola mundo");
console.log(nombreYApellido);
console.log("Hola " + nombreYApellido);
console.log(`Hola ${nombreYApellido}`);

console.log(sumar(1, 2));
console.log(restar(1, 2));
console.log(multiplicar(1, 2));
console.log(dividir(1, 2));
console.log(potencia(pi, 2));

const alumno1 = new Alumno('Juan', 'Perez', 30, '12345678');
const alumno1string = JSON.stringify(alumno1);
alumno1.saludar();
cambiarAdentroDelArchivo(alumno1string, './alumno.txt');