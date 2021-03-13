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
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        public async Task<IActionResult> IniciarInspeccion(int numeroOrden)
        {
           return await Iniciar(numeroOrden);
        }

        public async Task<IActionResult> Iniciar(int numeroOrden, Defecto defecto =null, int cantidadPrimera =-1)
        {
            var usuario = await ObtenerUsuario();

            var specOrden = new OrdenConNumeroSpecification(numeroOrden);
            var ordenCorrecta = await _unitOfWork.Repository<OrdenDeProduccion>().GetEntityWithSpec(specOrden);

            if (ordenCorrecta != null)
            {
                var empleadoValido = ordenCorrecta.VerificarAsosiacion(usuario.Empleado);

                if (empleadoValido)
                {
                    var specObservados = new DefectosObservadosSpecification(TipoDefecto.Observado);
                    var specReproceso = new DefectosObservadosSpecification(TipoDefecto.Reproceso);

                    var defectosObservados = await _unitOfWork.Repository<Defecto>().ListAsync(specObservados);
                    var defectosReproceso = await _unitOfWork.Repository<Defecto>().ListAsync(specReproceso);

                    var hora = _unitOfWork.GetHora();

                    var turnos = await _unitOfWork.Repository<Turno>().ListAllAsync();
                    var turno = ordenCorrecta.Linea.ValidarTurno(turnos, hora);

                    var specHorarios = new HorariosDeOrdenSpecification(ordenCorrecta.Id);
                    var horarios = await _unitOfWork.Repository<HorarioTrabajo>().ListAsync(specHorarios);

                    var defectosListViewModel = new DefectosListViewModel
                    {
                        DefectosObservados = defectosObservados,
                        DefectosReprocesos = defectosReproceso,
                        Hora = hora,
                        Orden = ordenCorrecta,
                        Turno = turno,
                    };


                    if (horarios.Count > 0)
                    {
                        if (horarios.Count == 0)
                        {
                            var hallazgo = new Hallazgo();
                            defectosListViewModel.Hallazgo = hallazgo;
                        }
                        else
                        {
                            defectosListViewModel.Hallazgo = horarios.Last().Hallazgos
                                .SingleOrDefault(x => x.Defecto == defecto);
                        }
                        defectosListViewModel.CantidadPrimera = horarios.Last().ParesPrimera.Last().Cantidad; ;
                    }

                    return View("IniciarInspeccion", defectosListViewModel);
                }
            }

            return View("IniciarInspeccion",new DefectosListViewModel { Mensaje = "El empleado no esta asociado a la orden" });
        }
        private async Task<Usuario> ObtenerUsuario()
        {
            var sessionUser = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("SessionUser"));
            var specUsuario = new ValidarEmpleadoSpecification(sessionUser.User);
            var usuario = await _unitOfWork.Repository<Usuario>().GetEntityWithSpec(specUsuario);

            return usuario;
        }

        public async Task<IActionResult> AgregarParDePrimera(int numeroOrden)
        {
            var usuario = await ObtenerUsuario();
            var cantidadPrimera = 0;

                var specOrden = new OrdenConNumeroSpecification(numeroOrden);
                var ordenCorrecta = await _unitOfWork.Repository<OrdenDeProduccion>().GetEntityWithSpec(specOrden);

                
                if (ordenCorrecta != null && usuario !=null)
                {
                    var specHorarios = new HorariosDeOrdenSpecification(ordenCorrecta.Id);
                    var horarios =await _unitOfWork.Repository<HorarioTrabajo>().ListAsync(specHorarios);
                    if (horarios.Count>0)
                    {
                        var hora = _unitOfWork.GetHora();
                        var cantidad = 1;

                        ordenCorrecta.AgregarParDePrimera(hora, cantidad, usuario.Empleado,horarios);
                        await _unitOfWork.Complete();
                }
                    
                }
                return await Iniciar(numeroOrden);
        }

        public async Task<IActionResult> RemoverParDePrimera(int numeroOrden)
        {
            var usuario = await ObtenerUsuario();
            var cantidadPrimera = 0;

            var specOrden = new OrdenConNumeroSpecification(numeroOrden);
            var ordenCorrecta = await _unitOfWork.Repository<OrdenDeProduccion>().GetEntityWithSpec(specOrden);


            if (ordenCorrecta != null && usuario != null)
            {
                var specHorarios = new HorariosDeOrdenSpecification(ordenCorrecta.Id);
                var horarios = await _unitOfWork.Repository<HorarioTrabajo>().ListAsync(specHorarios);
                if (horarios.Count > 0)
                {
                    var hora = _unitOfWork.GetHora();
                    var cantidad = 1;

                    ordenCorrecta.RemoverParDePrimera(horarios);
                    await _unitOfWork.Complete();
                }

            }
            return await Iniciar(numeroOrden);
        }

        public async Task<IActionResult> AgregarDefecto(int numeroOrden, Hallazgo hallazgo
            , string defectoDescripcion, TipoDefecto tipoDefecto, TipoPie tipoPie)
        {
            var usuario = await ObtenerUsuario();

            var specDefecto = new DefectoDescripcionSpecification(defectoDescripcion);
            var defecto = await _unitOfWork.Repository<Defecto>().GetEntityWithSpec(specDefecto);

                var specOrden = new OrdenConNumeroSpecification(numeroOrden);
                var ordenCorrecta = await _unitOfWork.Repository<OrdenDeProduccion>()
                    .GetEntityWithSpec(specOrden);


                if (ordenCorrecta != null && usuario!= null)
                {
                    var specHorarios = new HorariosDeOrdenSpecification(ordenCorrecta.Id);
                    var horarios = await _unitOfWork.Repository<HorarioTrabajo>().ListAsync(specHorarios);

                    hallazgo.TipoPie = tipoPie;
                    hallazgo.Hora = _unitOfWork.GetHora();
                    hallazgo.Cantidad = 1;
                    ordenCorrecta.AgregarDefecto(hallazgo, defecto, horarios);
                    await _unitOfWork.Complete();
                }
                return await Iniciar(numeroOrden, defecto);
        }

       
    }
}
