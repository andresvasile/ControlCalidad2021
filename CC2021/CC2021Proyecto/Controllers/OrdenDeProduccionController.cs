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
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ObtenerLineas()
        {
            var lineas = await _unitOfWork.Repository<LineaDeTrabajo>().ListAllAsync();

            return View(lineas);
        }


        public async Task<IActionResult> IniciarNuevaOrden()
        {
            var colores = await _unitOfWork.Repository<Color>().ListAllAsync();
            var modelos = await _unitOfWork.Repository<Modelo>().ListAllAsync();
            var lineas = await _unitOfWork.Repository<LineaDeTrabajo>().ListAllAsync();
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
                Modelos = modelos
            };

                ViewBag.ListPrueba = colores;
            return View(OrdenIniciarViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CrearOrden(int numeroLinea, int numeroOrden,
            string codigoModelo, int numeroColor)
        {
            var sessionUser = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("SessionUser"));
                

            var specLinea = new LineaConNumeroSpecification(numeroLinea);
            var linea = await _unitOfWork.Repository<LineaDeTrabajo>().GetEntityWithSpec(specLinea);

            var specModelo = new ModeloConNumeroSpecification(codigoModelo);
            var modelo = await _unitOfWork.Repository<Modelo>().GetEntityWithSpec(specModelo);

            var specColor = new ColorConNumeroSpecification(numeroColor);
            var color = await _unitOfWork.Repository<Color>().GetEntityWithSpec(specColor);

            var specUsuario = new ValidarEmpleadoSpecification(sessionUser.User);
            var usuario = await _unitOfWork.Repository<Usuario>().GetEntityWithSpec(specUsuario);

            var turnos = await _unitOfWork.Repository<Turno>().ListAllAsync();
            var horaActual =  _unitOfWork.GetHora();


            var estadoFinalizada = EstadoOrden.Finalizada;

            if (linea != null && modelo !=null && color != null)
            {
                var lineaDisponible = linea.ObtenerLineaSiEstaFinalizada(estadoFinalizada);
                if (lineaDisponible)
                {
                    var validado = 
                        linea.CrearOrden(numeroOrden,modelo,color,usuario.Empleado,turnos,horaActual);
                    if (validado)
                    {
                        _unitOfWork.Repository<LineaDeTrabajo>().Update(linea);
                        await _unitOfWork.Complete();

                        return View(linea);
                    }
                }
            }

            return RedirectToAction("ObtenerLineas");
        }

        public async Task<IActionResult> PausarOrden(LineaDeTrabajo linea)
        {
            var specLinea = new LineaConNumeroSpecification(linea.Numero);
            var lineaValida = await _unitOfWork.Repository<LineaDeTrabajo>().GetEntityWithSpec(specLinea);

            var specOrden = new OrdenConEstadoActivoSpecification(lineaValida,EstadoOrden.Activa);
            var ordenActiva = await _unitOfWork.Repository<OrdenDeProduccion>().GetEntityWithSpec(specOrden);

            var horaActual = _unitOfWork.GetHora();

            if (lineaValida!=null && ordenActiva != null)
            {
                lineaValida.PausarOrden(horaActual, ordenActiva);
                _unitOfWork.Repository<LineaDeTrabajo>().Update(lineaValida);
                await _unitOfWork.Complete();
            }

            return RedirectToAction("ObtenerLineas");

        }

        public async Task<IActionResult> ReanudarOrden(LineaDeTrabajo linea)
        {
            var specLinea = new LineaConNumeroSpecification(linea.Numero);
            var lineaValida = await _unitOfWork.Repository<LineaDeTrabajo>().GetEntityWithSpec(specLinea);

            var specOrden = new OrdenConEstadoActivoSpecification(lineaValida, EstadoOrden.Activa);
            var ordenActiva = await _unitOfWork.Repository<OrdenDeProduccion>().GetEntityWithSpec(specOrden);

            var horaActual = _unitOfWork.GetHora();
            var turnos = _unitOfWork.Repository<Turno>().ListAllAsync();
            

            if (lineaValida != null && ordenActiva != null && turnos != null)
            {
                lineaValida.ReanudarOrden(turnos.Result, horaActual, ordenActiva);
                _unitOfWork.Repository<LineaDeTrabajo>().Update(lineaValida);
                await _unitOfWork.Complete();
            }

            return RedirectToAction("ObtenerLineas");
        }

        public async Task<IActionResult> FinalizarOrden(LineaDeTrabajo linea)
        {
            var specLinea = new LineaConNumeroSpecification(linea.Numero);
            var lineaValida = await _unitOfWork.Repository<LineaDeTrabajo>().GetEntityWithSpec(specLinea);

            var specOrden = new OrdenConEstadoActivoSpecification(lineaValida, EstadoOrden.Activa);
            var ordenActiva = await _unitOfWork.Repository<OrdenDeProduccion>().GetEntityWithSpec(specOrden);

            var horaActual = _unitOfWork.GetHora();

            if (lineaValida != null && ordenActiva != null)
            {
                lineaValida.FinalizarOrden(horaActual, ordenActiva);
                _unitOfWork.Repository<LineaDeTrabajo>().Update(lineaValida);
                await _unitOfWork.Complete();
            }
            return RedirectToAction("ObtenerLineas");
        }
    }
}
