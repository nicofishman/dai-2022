import config from "../../dbconfig.js";
import sql from "mssql";

export default class PizzaService {
    getAll = async () => {
        try {
            const pool = await sql.connect(config);
            const result = await pool.request().query("SELECT * FROM Pizzas");
            return result.recordset;
        } catch (err) {
            console.log(err);
        }
    }
    getById = async (id) => {
        try {
            const pool = await sql.connect(config);
            const result = await pool.request()
                .input("id", sql.Int, id)
                .query("SELECT * FROM Pizzas WHERE id = @id");
            return result.recordset;
        } catch (err) {
            console.log(err);
        }
    }
    insert = async (pizza) => {
        try {
            const pool = await sql.connect(config);
            const result = await pool.request()
                .input("nombre", sql.VarChar, pizza.nombre)
                .input("libreGluten", sql.Bit, pizza.libreGluten)
                .input("importe", sql.Decimal(10, 2), pizza.importe)
                .input("descripcion", sql.VarChar, pizza.descripcion)
                .query("INSERT INTO Pizzas (nombre, libreGluten, importe, descripcion) VALUES (@nombre, @libreGluten, @importe, @descripcion)");
            return result.rowsAffected;
        } catch (err) {
            console.log(err);
        }
    }
    update = async (pizza) => {
        try {
            const pool = await sql.connect(config);
            const result = await pool.request()
                .input("id", sql.Int, pizza.id)
                .input("nombre", sql.VarChar, pizza.nombre)
                .input("libreGluten", sql.Bit, pizza.libreGluten)
                .input("importe", sql.Decimal(10, 2), pizza.importe)
                .input("descripcion", sql.VarChar, pizza.descripcion)
                .query("UPDATE Pizzas SET nombre = @nombre, libreGluten = @libreGluten, importe = @importe, descripcion = @descripcion WHERE id = @id");
            return result.rowsAffected;
        } catch (err) {
            console.log(err);
        }
    }
    deleteById = async (id) => {
        try {
            const pool = await sql.connect(config);
            const result = await pool.request()
                .input("id", sql.Int, id)
                .query("DELETE FROM Pizzas WHERE id = @id");
            return result.rowsAffected;
        } catch (err) {
            console.log(err);
        }

    }
}