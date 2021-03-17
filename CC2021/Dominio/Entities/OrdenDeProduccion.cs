using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Resources;
using System.Threading.Tasks;
using Dominio.Entities.Enums;

namespace Dominio.Entities
{
    public class OrdenDeProduccion : BaseEntity
    {
        public int Numero{ get; set; }
        public DateTime FechaInicio{ get; set; }
        public DateTime FechaFin { get; set; }
        public virtual Empleado LineaIniciadaPor { get; set; }

        public virtual Empleado EmpleadoOrden
        {
            get;
            set;
        }
        private Empleado _empleadoOrden { get; set; }
        public virtual Color Color{ get; set; }
        public int ColorId{ get; set; }
        public virtual Modelo Modelo{ get; set; }
        public int Objetivo { get; set; }
        public int ModeloId { get; set; }
        public EstadoOrden Estado { get; set; }
        public virtual List<HorarioTrabajo> Horarios{ get; set; }
        public int LineaId { get; set; }
        public virtual LineaDeTrabajo Linea{ get; set; }
        public OrdenDeProduccion()
        {
            
        }
        public OrdenDeProduccion(int numeroOrden, Color color, Modelo modelo, Turno turnoValidado
            , LineaDeTrabajo linea, DateTime horaActual)
        {

            if (Horarios == null)
            {
                Horarios = new List<HorarioTrabajo>();
            }

            Numero = numeroOrden;
            Color = color;
            ColorId = color.Id;
            Estado = EstadoOrden.Activa;
            FechaInicio = horaActual;
            Linea = linea;
            LineaId = linea.Id;
            LineaIniciadaPor = linea.Empleado;
            Modelo = modelo;
            ModeloId = modelo.Id;
            Objetivo = modelo.Objetivo;

            HorarioNuevo(turnoValidado, horaActual);
        }

        public void PausarOrden(in DateTime horaActual)
        {
            Estado = EstadoOrden.Pausada;
            Horarios.Last().Fin = horaActual;
            Horarios.Last().OrdenDeProduccion = this;
            Horarios.Last().OrdenDeProduccionId = Id;

        }

        public void ReanudarOrden(Turno turnoValidado, in DateTime horaActual)
        {
            Estado = EstadoOrden.Activa;
            HorarioNuevo(turnoValidado, horaActual);
        }
        private void HorarioNuevo(Turno turnoValidado, DateTime horaActual)
        {
            var horarioNuevo = new HorarioTrabajo
            {
                Turno = turnoValidado,
                Inicio = horaActual,
                OrdenDeProduccion = this,
                OrdenDeProduccionId = Id
                
            };
            Horarios.Add(horarioNuevo);
        }

        public void FinalizarOrden(in DateTime horaActual)
        {
            Estado = EstadoOrden.Finalizada;
            Horarios.Last().Fin = horaActual;
            FechaFin = horaActual;
        }

        public void AsociarSupervisor(Empleado empleadoOrden) 
        {
            EmpleadoOrden = empleadoOrden;
        }

        public void DesasociarSupervisor(Empleado empleado)
        {
            if (EmpleadoOrden == empleado)
            {
                EmpleadoOrden = null;
            }
        }

        public void AgregarParDePrimera(DateTime hora, int cantidad, Empleado empleado, List<HorarioTrabajo> horarios)
        {
                var prim = new Primera
                {
                    EmpleadoDeCalidad = empleado,
                    Cantidad = cantidad,
                    Hora = hora
                };
                horarios.Last().ParesPrimera.Add(prim);
        }
        public void RemoverParDePrimera(List<HorarioTrabajo> horarios)
        {
            if (horarios.Last().ParesPrimera.Count>0)
            {
                horarios.Last().ParesPrimera.RemoveAt(horarios.Last().ParesPrimera.Count - 1);
            }
            
        }
        public bool VerificarAsosiacion(Empleado empleado)
        {
            if (EmpleadoOrden == empleado)
            {
                return true;
            }

            return false;
        }

        public void AgregarDefecto(TipoPie tipoPie,DateTime hora, Empleado empleado
            , int cantidad, Defecto defecto, List<HorarioTrabajo> horarios)
        {
            
                var hallaz = new Hallazgo()
                {
                    EmpleadoDeCalidad = empleado,
                    Hora = hora,
                    Defecto = defecto,
                    Cantidad = cantidad,
                    TipoPie = tipoPie,

                };
                horarios.Last().Hallazgos.Add(hallaz);
        }


        public void RemoverDefecto(TipoPie tipoPie, Empleado usuarioEmpleado, Defecto defecto, List<HorarioTrabajo> horarios)
        {
            var h = horarios.Last().Hallazgos.Any(x => x.Defecto == defecto
                                               && x.EmpleadoDeCalidad == usuarioEmpleado
                                               && x.TipoPie==tipoPie);
            if (h)
            {
                if(horarios.Last().Hallazgos.Any())
                {
                    horarios.Last().Hallazgos.RemoveAt(horarios.Last().Hallazgos.Count - 1);
                }
            }
        }
    }
}