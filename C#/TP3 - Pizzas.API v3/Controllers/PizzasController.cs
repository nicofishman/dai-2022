using Pizzas.API.Models;
using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pizzas.API.Services;
using Pizzas.API.Utils;

namespace Pizzas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PizzasController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(PizzaService.GetAll());
            }
            catch (Exception ex)
            {
                MethodBase m = MethodBase.GetCurrentMethod();
                CustomLog.LogError(ex, m.DeclaringType.Name, m.Name);
                return Problem(
                    detail: ex.Message,
                    title: "Error al obtener las pizzas",
                    statusCode: 500
                );
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                Pizza pizza = PizzaService.GetById(id);
                if (pizza == null)
                {
                    return NotFound();
                }
                return Ok(pizza);
            }
            catch (Exception ex)
            {
                MethodBase m = MethodBase.GetCurrentMethod();
                CustomLog.LogError(ex, m.DeclaringType.Name, m.Name);
                return Problem(
                    detail: ex.Message,
                    title: "Error al obtener la pizza",
                    statusCode: 500
                );
            }
        }

        [HttpPost]
        public IActionResult Create(Pizza pizza)
        {
            if (pizza == null)
            {
                return BadRequest();
            }
            try
            {
                string token = Request.Headers["token"];
                int rowsAffected = PizzaService.Add(pizza, token);
                if (rowsAffected == -1)
                {
                    return Unauthorized();
                }
                return CreatedAtAction(nameof(Create), new { id = pizza.Id }, pizza);
            }
            catch (Exception ex)
            {
                MethodBase m = MethodBase.GetCurrentMethod();
                CustomLog.LogError(ex, m.DeclaringType.Name, m.Name);
                return Problem(
                    detail: ex.Message,
                    title: "Error al crear la pizza",
                    statusCode: 500
                );
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Pizza pizza)
        {
            if (pizza == null || pizza.Id != id)
            {
                return BadRequest();
            }
            try
            {

                Pizza pizzaToUpdate = PizzaService.GetById(id);
                if (pizzaToUpdate == null)
                {
                    return NotFound();
                }
                else
                {
                    string token = Request.Headers["token"];
                    int intRowsAffected = PizzaService.UpdateById(pizza, token);
                    if (intRowsAffected == 0)
                    {
                        return Unauthorized();
                    }
                    if (intRowsAffected == -1)
                    {
                        return Unauthorized();
                    }
                    if (intRowsAffected > 0)
                    {
                        return Ok(pizza);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                MethodBase m = MethodBase.GetCurrentMethod();
                CustomLog.LogError(ex, m.DeclaringType.Name, m.Name);
                return Problem(
                    detail: ex.Message,
                    title: "Error al actualizar la pizza",
                    statusCode: 500
                );
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            try
            {
                Pizza pizza = PizzaService.GetById(id);
                if (pizza == null)
                {
                    return NotFound();
                }
                else
                {
                    string token = Request.Headers["token"];
                    int intRowsAffected = PizzaService.DeleteById(id, token);
                    if (intRowsAffected == -1)
                    {
                        return Unauthorized();
                    }
                    if (intRowsAffected > 0)
                    {
                        return Ok(pizza);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                MethodBase m = MethodBase.GetCurrentMethod();
                CustomLog.LogError(ex, m.DeclaringType.Name, m.Name);
                return Problem(
                    detail: ex.Message,
                    title: "Error al eliminar la pizza",
                    statusCode: 500
                );
            }
        }
    }
}