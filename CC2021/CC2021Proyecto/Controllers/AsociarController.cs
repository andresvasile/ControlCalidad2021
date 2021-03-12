using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Specifications;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CC2021Proyecto.Controllers
{
    public class AsociarController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AsociarController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
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

        public async Task<IActionResult> AsociarSupervisor(int numeroOrden, string user)
        {
            var specUsuario = new ValidarEmpleadoSpecification(user);
            var usuario = _unitOfWork.Repository<Usuario>().GetEntityWithSpec(specUsuario);

            var specOrden = new OrdenConNumeroSpecification(numeroOrden);
            var orden = _unitOfWork.Repository<OrdenDeProduccion>().GetEntityWithSpec(specOrden);

            var lineas = _unitOfWork.Repository<LineaDeTrabajo>().ListAllAsync();

            if (orden != null && usuario.Result.Empleado != null)
            {
                foreach (var linea in lineas.Result)
                {
                    var ordenAsociada = linea.ValidarSupervisor(orden.Result, usuario.Result.Empleado);
                    if (ordenAsociada != null)
                    {
                        _unitOfWork.Repository<OrdenDeProduccion>().Update(ordenAsociada);
                        await _unitOfWork.Complete();
                    }
                }
            }

            var specOrdenActualizada = new OrdenConNumeroSpecification(orden.Result.Numero);
            var ordenActualizada = _unitOfWork.Repository<OrdenDeProduccion>().GetEntityWithSpec(specOrdenActualizada);

            return RedirectToAction("IniciarInspeccion", "ControlCalidad", new { ordenActualizada });
        }

        public async Task<IActionResult> DesasociarSupervisor(int numeroOrden, string user)
        {
            var specUsuario = new ValidarEmpleadoSpecification(user);
            var usuario = _unitOfWork.Repository<Usuario>().GetEntityWithSpec(specUsuario);

            var specOrden = new OrdenConNumeroSpecification(numeroOrden);
            var orden = _unitOfWork.Repository<OrdenDeProduccion>().GetEntityWithSpec(specOrden);

            var lineas = _unitOfWork.Repository<LineaDeTrabajo>().ListAllAsync();

            if (orden != null && usuario.Result.Empleado != null)
            {
                foreach (var linea in lineas.Result)
                {
                    var ordenDesasociada = linea.DesasociarSupervisor(orden.Result, usuario.Result.Empleado);
                    if (ordenDesasociada != null)
                    {
                        _unitOfWork.Repository<OrdenDeProduccion>().Update(ordenDesasociada);
                        await _unitOfWork.Complete();
                    }
                }
            }
            return RedirectToAction("IniciarAsociacion");
        }
    }
}
