using Pizzas.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pizzas.API.Services;

namespace Pizzas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PizzasController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(PizzaService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Pizza pizza = PizzaService.GetById(id);
            if (pizza == null)
            {
                return NotFound();
            }
            return Ok(pizza);
        }

        [HttpPost]
        public IActionResult Create(Pizza pizza)
        {
            if (pizza == null)
            {
                return BadRequest();
            }
            string token = Request.Headers["token"];
            int rowsAffected = PizzaService.Add(pizza, token);
            if (rowsAffected == -1)
            {
                return Unauthorized();
            }
            return CreatedAtAction(nameof(Create), new { id = pizza.Id }, pizza);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Pizza pizza)
        {
            if (pizza == null || pizza.Id != id)
            {
                return BadRequest();
            }
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

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
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
    }
}