using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgendaDeTurnos.Data;
using AgendaDeTurnos.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace AgendaDeTurnos.Controllers
{
    [AllowAnonymous]
    public class UsuariosController : Controller
    {
        private readonly AgendaDeTurnosContext _context;
        private readonly ISeguridad seguridad = new SeguridadBasica();

        public UsuariosController(AgendaDeTurnosContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public IActionResult Ingresar(string returnUrl)
        {
            TempData["UrlIngreso"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Ingresar(string email, string pass)
        {
            // Guardamos la URL a la que debemos redirigir al usuario
            var urlIngreso = TempData["UrlIngreso"] as string;

            // Verificamos que ambos esten informados
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(pass))
            {

                // Verificamos que exista el usuario
                var user = await _context.Usuario.FirstOrDefaultAsync(u => u.Email == email);
                if (user != null)
                {
                    var passEncriptada = seguridad.EncriptarPass(pass);
                    if (passEncriptada.SequenceEqual(user.Password))
                    {

                        // Creamos los Claims (credencial de acceso con informacion del usuario)
                        ClaimsIdentity identidad = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                        // Agregamos a la credencial el email del usuario
                        identidad.AddClaim(new Claim(ClaimTypes.Name, email));
                        // Agregamos a la credencial el nombre del estudiante/administrador
                        identidad.AddClaim(new Claim(ClaimTypes.GivenName, user.Nombre));
                        // Agregamos a la credencial el Rol
                        identidad.AddClaim(new Claim(ClaimTypes.Role, user.Rol.ToString()));
                        // Agregarmos el Id de Usuario
                        identidad.AddClaim(new Claim("UserId", user.Id.ToString()));

                        ClaimsPrincipal principal = new ClaimsPrincipal(identidad);

                        // Ejecutamos el Login
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                        if (!string.IsNullOrEmpty(urlIngreso))
                        {
                            return Redirect(urlIngreso);
                        }
                        else
                        {
                            // Redirigimos a la pagina principal
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            }

            ViewBag.ErrorEnLogin = "Verifique el email y contraseña";
            TempData["UrlIngreso"] = urlIngreso; // Volvemos a enviarla en el TempData para no perderla
            return View();

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrarse(Paciente paciente, string pass)
        {          
            //TODO: validar usuario existente
            if (ModelState.IsValid)
            {
                if(_context.Paciente.Any(p => p.Email == paciente.Email))
                {
                    ModelState.AddModelError(nameof(Paciente.Email),"El mail ya esta utilizado");
                }
                else
                {
                    if (seguridad.ValidarPass(pass))
                    {
                        paciente.Password = seguridad.EncriptarPass(pass);
                        paciente.Id = Guid.NewGuid();
                        _context.Add(paciente);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Ingresar));
                    }
                    else
                    {
                        ModelState.AddModelError(nameof(Paciente.Password), "No cumple con los requisitos");
                    }
                }                
            }
            return View(paciente);
        }
    }
}