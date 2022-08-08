using Pizzas.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
            return Ok(BD.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Pizza pizza = BD.GetById(id);
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
            int rowsAffected = BD.Add(pizza);
            return CreatedAtAction(nameof(Create), new { id = pizza.Id }, pizza);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Pizza pizza)
        {
            if (pizza == null || pizza.Id != id)
            {
                return BadRequest();
            }
            Pizza pizzaToUpdate = BD.GetById(id);
            if (pizzaToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                int intRowsAffected = BD.UpdateById(pizza);
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
        public IActionResult DeleteById(int id) {
            Pizza pizza = BD.GetById(id);
            if (pizza == null)
            {
                return NotFound();
            }
            else
            {
                int intRowsAffected = BD.DeleteById(id);
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