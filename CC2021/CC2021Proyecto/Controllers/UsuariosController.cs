using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API.Specifications;
using CC2021Proyecto.ValidationClass;
using Dominio.Entities;
using Dominio.Entities.Enums;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CC2021Proyecto.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsuariosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Autenticacion()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Autenticacion(UsuarioValidation usuarioValidation)
        {
            var specUsuario = new ValidarUsuarioSpecification(usuarioValidation.User, usuarioValidation.Password);
            var usuarioValidado = await _unitOfWork.Repository<Usuario>().GetEntityWithSpec(specUsuario);

            if (usuarioValidado != null)
            {
                HttpContext.Session.SetString("SessionUser",JsonConvert.SerializeObject(usuarioValidado));

                if (usuarioValidado.Empleado.Rol == TipoEmpleado.Administrativo)
                {
                    return RedirectToAction("Index", "GestionAdmin");
                }
                if (usuarioValidado.Empleado.Rol == TipoEmpleado.SupervisorDeCalidad)
                {
                    return RedirectToAction("IniciarAsociacion", "Asociar");
                }
                if (usuarioValidado.Empleado.Rol == TipoEmpleado.SupervisorDeLinea)
                {
                    return RedirectToAction("ObtenerLineas", "OrdenDeProduccion");
                }
            }
            
            var mensaje = "Error al autenticar";
            ModelState.AddModelError("", mensaje);

            return View();
        }

        public async Task<IActionResult> CerrarSesion()
        {
            var usuario = await ObtenerUsuario();
            if (usuario != null)
            {
                 HttpContext.Session.SetString("","");
            }
            var usuario2 = await ObtenerUsuario();

            return RedirectToAction("Autenticacion", "Usuarios");
        }

        private async Task<Usuario> ObtenerUsuario()
        {
            var sessionUser = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("SessionUser"));
            var specUsuario = new ValidarEmpleadoSpecification(sessionUser.User);
            var usuario = await _unitOfWork.Repository<Usuario>().GetEntityWithSpec(specUsuario);

            return usuario;
        }

        public IActionResult ErrorAlAutenticar(string mensaje)
        {
            return View(mensaje);
        }
    }
}
