using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using API.Specifications;
using Dominio.Entities;
using Dominio.Entities.Enums;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CC2021Proyecto.Controllers
{
    public class AsociarController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AsociarController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> IniciarAsociacion()
        {
            var spec = new OrdenesConOrdenActivaYPausadaSpecification();
            var ordenes = await _unitOfWork.Repository<OrdenDeProduccion>().ListAsync(spec);

            if (ordenes != null)
            {
                return View(ordenes);
            }

            return View();
        }

        public async Task<IActionResult> AsociarSupervisor(int numeroOrden)
        {

            var usuario = await ObtenerUsuario();

            var specOrden = new OrdenConEstadoActivaOPausadaYNumero(numeroOrden);
            var orden = await _unitOfWork.Repository<OrdenDeProduccion>().GetEntityWithSpec(specOrden);

            var specLineas = new AddLineasIncludesSpecification();
            var lineas = await _unitOfWork.Repository<LineaDeTrabajo>().ListAsync(specLineas);

            var supervisorValido = 0;
            var hayActivasOPausadas = 0;

            if (orden != null && usuario.Empleado != null && lineas != null)
            {
                foreach (var linea in lineas)
                {
                    if (linea.OrdenesDeProduccion.All(o => o.Estado == EstadoOrden.Finalizada)) {}
                    else
                    {
                        hayActivasOPausadas++;
                        var supervisorDispo = linea.ValidarSupervisor(usuario.Empleado);
                        if (supervisorDispo)
                        {
                            supervisorValido++;
                        }
                    }
                    
                }

                if (supervisorValido == hayActivasOPausadas)
                {
                    orden.AsociarSupervisor(usuario.Empleado);
                    await _unitOfWork.Complete();
                }
            }

            return RedirectToAction("IniciarAsociacion");
        }
        public async Task<IActionResult> DesasociarSupervisor(int numeroOrden)
        {
            var usuario = await ObtenerUsuario();

            var specOrden = new OrdenConEstadoActivaOPausadaYNumero(numeroOrden);
            var orden = await _unitOfWork.Repository<OrdenDeProduccion>().GetEntityWithSpec(specOrden);


            if (orden != null && usuario.Empleado != null )
            {
                orden.DesasociarSupervisor(usuario.Empleado);
                await _unitOfWork.Complete();
                
            }
            return RedirectToAction("IniciarAsociacion");
        }
        private async Task<Usuario> ObtenerUsuario()
        {
            var sessionUser = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("SessionUser"));
            var specUsuario = new ValidarEmpleadoSpecification(sessionUser.User);
            var usuario = await _unitOfWork.Repository<Usuario>().GetEntityWithSpec(specUsuario);

            return usuario;
        }
    }
}
