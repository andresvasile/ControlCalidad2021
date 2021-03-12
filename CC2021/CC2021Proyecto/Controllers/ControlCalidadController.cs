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
using Microsoft.AspNetCore.Mvc;

namespace CC2021Proyecto.Controllers
{
    public class ControlCalidadController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public ControlCalidadController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> IniciarInspeccion(OrdenDeProduccion orden, string user)
        {
            if (orden != null)
            {
                var specUser = new ValidarEmpleadoSpecification(user);
                var usuario = _unitOfWork.Repository<Usuario>().GetEntityWithSpec(specUser);

                var specOrden = new OrdenConNumeroSpecification(orden.Numero);
                var ordenCorrecta = _unitOfWork.Repository<OrdenDeProduccion>().GetEntityWithSpec(specOrden);

                var empleadoValido = ordenCorrecta.Result.VerificarAsosiacion(usuario.Result.Empleado);

                if (empleadoValido)
                {
                    var specObservados = new DefectosObservadosSpecification(TipoDefecto.Observado);
                    var specReproceso = new DefectosObservadosSpecification(TipoDefecto.Reproceso);

                    var defectosObservados = await _unitOfWork.Repository<Defecto>().ListAsync(specObservados);
                    var defectosReproceso = await _unitOfWork.Repository<Defecto>().ListAsync(specReproceso);

                    var hora = _unitOfWork.GetHora();

                    var defectosListViewModel = new DefectosListViewModel
                    {
                        DefectosObservados = defectosObservados,
                        DefectosReprocesos = defectosReproceso,
                        Hora = hora,
                        Orden = orden
                    };
                    return View(defectosListViewModel);
                }
            }

            return View(new DefectosListViewModel {Mensaje = "El empleado no esta asociado a la orden"});
        }

        public async Task<IActionResult> AgregarParDePrimera(OrdenDeProduccion orden, string user, Primera primera)
        {
            if (orden != null)
            {
                var specUser = new ValidarEmpleadoSpecification(user);
                var usuario = _unitOfWork.Repository<Usuario>().GetEntityWithSpec(specUser);

                var specOrden = new OrdenConNumeroSpecification(orden.Numero);
                var ordenCorrecta = _unitOfWork.Repository<OrdenDeProduccion>().GetEntityWithSpec(specOrden);


                if (ordenCorrecta.Result != null)
                {
                    primera.Hora = _unitOfWork.GetHora();
                    primera.Cantidad = 1;
                    primera.EmpleadoDeCalidad = usuario.Result.Empleado;
                    ordenCorrecta.Result.AgregarParDePrimera(primera);
                    _unitOfWork.Repository<OrdenDeProduccion>().Update(ordenCorrecta.Result);
                    await _unitOfWork.Complete();
                }
            }

            return View("IniciarInspeccion");
        }

        public async Task<IActionResult> AgregarDefecto(OrdenDeProduccion orden, string user, Hallazgo hallazgo)
        {
            if (orden != null)
            {
                var specUser = new ValidarEmpleadoSpecification(user);
                var usuario = await _unitOfWork.Repository<Usuario>().GetEntityWithSpec(specUser);

                var specOrden = new OrdenConNumeroSpecification(orden.Numero);
                var ordenCorrecta = await _unitOfWork.Repository<OrdenDeProduccion>().GetEntityWithSpec(specOrden);

                if (ordenCorrecta != null)
                {
                    hallazgo.Hora = _unitOfWork.GetHora();
                    hallazgo.Cantidad = 1;
                    ordenCorrecta.AgregarDefecto(hallazgo);
                    _unitOfWork.Repository<OrdenDeProduccion>().Update(ordenCorrecta);
                    await _unitOfWork.Complete();
                }
            }
            return View("IniciarInspeccion");
        }
    }
}
