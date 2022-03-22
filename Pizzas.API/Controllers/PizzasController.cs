using Pizzas.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Pizzas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PizzasController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(PizzasService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var pizza = Pizza.Pizzas.FirstOrDefault(p => p.Id == id);
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
            Pizza.Pizzas.Add(pizza);
            return CreatedAtRoute("GetById", new { id = pizza.Id }, pizza);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Pizza pizza)
        {
            if (pizza == null || pizza.Id != id)
            {
                return BadRequest();
            }
            var pizzaToUpdate = Pizza.Pizzas.FirstOrDefault(p => p.Id == id);
            if (pizzaToUpdate == null)
            {
                return NotFound();
            }
            pizzaToUpdate.Nombre = pizza.Nombre;
            pizzaToUpdate.Descripcion = pizza.Descripcion;
            pizzaToUpdate.LibreGluten = pizza.LibreGluten;
            return NoContent();
        }
    }
}