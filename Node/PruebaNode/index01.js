import { sumar, restar, dividir, multiplicar, potencia, raiz, pi } from './src/modules/matematica.js';
import Alumno from './index03.js';
import { copiar } from './index04.js';
import { decomposeUrl } from './url.js';
import { getCurrencyByISO } from './currency.js';

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
alumno1.saludar();
copiar('alumno.txt', './aaaaaa/alumno.txt');

console.log(decomposeUrl('http://www.ort.edu.ar:8080/alumnos/index.htm?curso=2022&mes=mayo'))

console.log(getCurrencyByISO('ET'))