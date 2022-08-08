using Pizzas.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pizzas.API.Services;

namespace Pizzas.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]

    public class UsuariosController : ControllerBase {
        [HttpPost] 
        [Route("login")] 
        public IActionResult Login(Usuario usuario){
            if(usuario == null){
                return BadRequest();
            }
            Usuario usuarioLogin = UsuarioService.Login(usuario.UserName, usuario.Password);
            if(usuarioLogin == null){
                return NotFound();
            }
            usuarioLogin.Password = new string('*', usuarioLogin.Password.Length);
            return Ok(usuarioLogin);
        }
    }
}