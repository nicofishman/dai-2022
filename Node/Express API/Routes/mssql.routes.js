import { Router } from "express";
import PizzaService from "../services/pizzaService.js";

const mssqlRouter = Router();
const svc = new PizzaService();

mssqlRouter.get("/", async (req, res) => {
    const pizzas = await svc.getAll()
    res.json(pizzas);
})

// get by id
mssqlRouter.get("/:id", async (req, res) => {
    const id = req.params.id;
    const pizza = await svc.getById(id);
    res.json(pizza);
})

// insert
mssqlRouter.post("/", async (req, res) => {
    const pizza = req.body;
    const result = await svc.insert(pizza);
    res.json(result);
})

// update
mssqlRouter.put("/:id", async (req, res) => {
    const id = req.params.id;
    const pizza = req.body;
    const result = await svc.update(pizza);
    res.json(result);
})

//delete by id
mssqlRouter.delete("/:id", async (req, res) => {
    const id = req.params.id;
    const result = await svc.deleteById(id);
    res.json(result);
})

export default mssqlRouter;