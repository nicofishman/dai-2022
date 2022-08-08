import PizzaService from './src/services/pizzaService.js';

const main = () => {
    const svc = new PizzaService();
    svc.getAll().then(console.log)
    svc.getById(1).then(console.log)
    // svc.insert({
    //     nombre: 'Pizza de queso',
    //     libreGluten: true,
    //     importe: 10.5,
    //     descripcion: 'Pizza de queso con tomate'
    // }).then(console.log)
    // svc.deleteById(11).then(console.log)
}

main()