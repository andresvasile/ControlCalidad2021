using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Specifications;
using CC2021Proyecto.ViewModels;
using Datos.Data.Interfaces;
using Dominio.Entities;
using Dominio.Entities.Enums;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CC2021Proyecto.Controllers
{
    public class OrdenDeProduccionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrdenDeProduccionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> ObtenerLineas()
        {
            var spec = new AddOrdersIncludesSpecification();
            var ordenes = await _unitOfWork.Repository<OrdenDeProduccion>().ListAsync(spec);

            return View(ordenes);
        }


        public async Task<IActionResult> IniciarNuevaOrden()
        {
            var colores = await _unitOfWork.Repository<Color>().ListAllAsync();
            var modelos = await _unitOfWork.Repository<Modelo>().ListAllAsync();
            var specLineas = new AddLineasIncludesSpecification();
            var lineas = await _unitOfWork.Repository<LineaDeTrabajo>().ListAsync(specLineas);
            var estadoFinalizada = EstadoOrden.Finalizada;
            var lineasDisponibles = new List<LineaDeTrabajo>();

            
                foreach (var linea in lineas)
                {
                    var validado = linea.ObtenerLineaSiEstaFinalizada(estadoFinalizada);
                    if (validado)
                    {
                        lineasDisponibles.Add(linea);
                    }
                }
                var OrdenIniciarViewModel = new OrdenIniciarViewModel
                {
                Colores = colores,
                Lineas = lineasDisponibles,
                Modelos = modelos,
                Coloress = colores,
                Modeloss=modelos,
                Lineass= lineasDisponibles
                };

                return View(OrdenIniciarViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CrearOrden(OrdenIniciarViewModel orden, int numeroOrden)
        {
            var usuario = await ObtenerUsuario();

            var specLinea = new LineaConIdSpecification(Int32.Parse(orden.LineaSelected));
            var linea = await _unitOfWork.Repository<LineaDeTrabajo>().GetEntityWithSpec(specLinea);

            var specModelo = new ModeloConNumeroSpecification(orden.ModeloSelected);
            var modelo = await _unitOfWork.Repository<Modelo>().GetEntityWithSpec(specModelo);

            var specColor = new ColorConNumeroSpecification(Int32.Parse(orden.ColorSelected));
            var color = await _unitOfWork.Repository<Color>().GetEntityWithSpec(specColor);

            var turnos = await _unitOfWork.Repository<Turno>().ListAllAsync();
            var horaActual =  _unitOfWork.GetHora();

            var estadoFinalizada = EstadoOrden.Finalizada;

            var specLineas = new AddLineasIncludesSpecification();
            var lineas = await _unitOfWork.Repository<LineaDeTrabajo>().ListAsync(specLineas);

            int usuarioValidado = 0;

            if (ModelState.IsValid)
            {
                foreach (var li in lineas)
                {
                    var validacionDeEmpleado = li.ComprobarAsignacionSupervisor(usuario.Empleado);
                    if (validacionDeEmpleado)
                    {
                        usuarioValidado++;
                    }
                }

                if (linea != null && modelo != null && color != null && usuario != null && usuarioValidado==lineas.Count)
                {
                    var lineaDisponible = linea.ObtenerLineaSiEstaFinalizada(estadoFinalizada);
                    if (lineaDisponible)
                    {
                        var spec = new OrdenConNumeroSpecification(numeroOrden);
                        var ordenExistente = await _unitOfWork.Repository<OrdenDeProduccion>()
                            .GetEntityWithSpec(spec);
                        if (ordenExistente == null)
                        {
                            var ordenV =
                                linea.CrearOrden(numeroOrden, modelo, color, usuario.Empleado, turnos, horaActual);
                            if (ordenV != null)
                            {
                                _unitOfWork.Repository<LineaDeTrabajo>().Update(linea);
                                await _unitOfWork.Complete();
                            }
                            ModelState.AddModelError("","Se debe crear la orden en un turno valido ");
                        }
                        ModelState.AddModelError("", "Orden ya existente, ingrese otra");
                    }
                    ModelState.AddModelError("", "Orden activa en linea" + linea.Numero + ", finalice la ultima para crear otra");
                }
            }
           
            return RedirectToAction("ObtenerLineas");
        }

        private async Task<Usuario> ObtenerUsuario()
        {
            var sessionUser = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("SessionUser"));
            var specUsuario = new ValidarEmpleadoSpecification(sessionUser.User);
            var usuario = await _unitOfWork.Repository<Usuario>().GetEntityWithSpec(specUsuario);

            return usuario;
        }

        [HttpPost]
        public async Task<IActionResult> PausarOrden(int numeroLinea)
        {
            var usuario = await ObtenerUsuario();

            if (usuario != null)
            {
                var specLinea = new LineaConNumeroYEmpleadoSpecification(numeroLinea, usuario.Empleado);
                var lineaValida = await _unitOfWork.Repository<LineaDeTrabajo>().GetEntityWithSpec(specLinea);

                if (lineaValida != null)
                {
                    var specOrden = new OrdenConEstadoABuscarSpecification(lineaValida, EstadoOrden.Activa);
                    var ordenActiva = await _unitOfWork.Repository<OrdenDeProduccion>().GetEntityWithSpec(specOrden);

                    var horaActual = _unitOfWork.GetHora();

                    if (ordenActiva != null)
                    {
                        lineaValida.PausarOrden(horaActual, ordenActiva);
                        await _unitOfWork.Complete();
                    }
                }
            }
            return RedirectToAction("ObtenerLineas");
        }

        public async Task<IActionResult> ReanudarOrden(int numeroLinea)
        {
            var usuario = await ObtenerUsuario();

            if (usuario != null)
            {
                var specLinea = new LineaConNumeroYEmpleadoSpecification(numeroLinea, usuario.Empleado);
                var lineaValida = await _unitOfWork.Repository<LineaDeTrabajo>().GetEntityWithSpec(specLinea);

                if (lineaValida != null)
                {
                    var specOrden = new OrdenConEstadoABuscarSpecification(lineaValida, EstadoOrden.Pausada);
                    var ordenActiva = await _unitOfWork.Repository<OrdenDeProduccion>().GetEntityWithSpec(specOrden);

                    var horaActual = _unitOfWork.GetHora();
                    var turnos = await _unitOfWork.Repository<Turno>().ListAllAsync();


                    if (ordenActiva != null && turnos != null && usuario != null)
                    {
                        lineaValida.ReanudarOrden(turnos, horaActual, ordenActiva);
                        await _unitOfWork.Complete();
                    }
                }
            }
            return RedirectToAction("ObtenerLineas");
        }

        public async Task<IActionResult> FinalizarOrden(int numeroLinea)
        {
            var usuario = await ObtenerUsuario();

            if (usuario != null)
            {
                var specLinea = new LineaConNumeroYEmpleadoSpecification(numeroLinea, usuario.Empleado);
                var lineaValida = await _unitOfWork.Repository<LineaDeTrabajo>().GetEntityWithSpec(specLinea);
                if (lineaValida != null)
                {
                    var specOrden = new OrdenesConOrdenActivaYPausadaConLineaSpecification(lineaValida);
                    var ordenActivaOPausada = await _unitOfWork.Repository<OrdenDeProduccion>().GetEntityWithSpec(specOrden);

                    var horaActual = _unitOfWork.GetHora();

                    if (ordenActivaOPausada != null)
                    {
                        lineaValida.FinalizarOrden(horaActual, ordenActivaOPausada);
                        await _unitOfWork.Complete();
                    }
                    ModelState.AddModelError("","Orden ya finalizada");
                }
            }

            return RedirectToAction("ObtenerLineas");
        }
    }
}
