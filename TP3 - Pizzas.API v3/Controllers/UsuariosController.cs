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

    public class UsuariosController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public IActionResult Login(Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest();
            }
            try
            {
                Usuario usuarioLogin = UsuarioService.Login(usuario.UserName, usuario.Password);
                if (usuarioLogin == null)
                {
                    return NotFound();
                }
                usuarioLogin.Password = new string('*', usuarioLogin.Password.Length);
                return Ok(usuarioLogin);
            }
            catch (Exception ex)
            {
                MethodBase m = MethodBase.GetCurrentMethod();
                CustomLog.LogError(ex, m.DeclaringType.Name, m.Name);
                return Problem(
                    detail: ex.Message,
                    title: "Error al obtener el usuario",
                    statusCode: 500
                );
            }
        }
    }
}