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
        public Empleado LineaIniciadaPor { get; set; }

        public Empleado EmpleadoOrden
        {
            get { return _empleadoOrden;}

            set
            {
                if (EmpleadoOrden.Rol == TipoEmpleado.SupervisorDeCalidad)
                {
                    _empleadoOrden = value;
                }
            }
        }
        private Empleado _empleadoOrden { get; set; }
        public Color Color{ get; set; }
        public int ColorId{ get; set; }
        public Modelo Modelo{ get; set; }
        public int Objetivo { get; set; }
        public int ModeloId { get; set; }
        public EstadoOrden Estado { get; set; }
        public List<HorarioTrabajo> Horarios{ get; set; }
        public int LineaId { get; set; }
        public LineaDeTrabajo Linea{ get; set; }
        public OrdenDeProduccion()
        {
            
        }
        public OrdenDeProduccion(int numeroOrden, Color color, Modelo modelo, Turno turnoValidado
            , LineaDeTrabajo linea, DateTime horaActual)
        {

            if (Horarios == null) Horarios = new List<HorarioTrabajo>();

            Numero = numeroOrden;
            Color = color;
            ColorId = color.Id;
            Estado = EstadoOrden.Activa;
            FechaInicio = turnoValidado.Inicio;
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
                OrdenDeProduccionId = this.Id
            };
            Horarios.Add(horarioNuevo);
        }

        public void FinalizarOrden(in DateTime horaActual)
        {
            Estado = EstadoOrden.Finalizada;
            Horarios.Last().Fin = horaActual;
        }

        public void AsociarSupervisor(Empleado empleadoOrden) 
        {
            if (EmpleadoOrden != null)
            {
                EmpleadoOrden = empleadoOrden;
            }
            
        }

        public void DesasociarSupervisor(Empleado empleado)
        {
            if (EmpleadoOrden == empleado)
            {
                EmpleadoOrden = null;
            }
        }

        public void AgregarParDePrimera(Primera primera)
        {
            var prim = new Primera
            {
                EmpleadoDeCalidad = primera.EmpleadoDeCalidad,
                Cantidad = primera.Cantidad,
                Hora = primera.Hora
            };
            if (Horarios.Count > 0)
            {
                Horarios.LastOrDefault().ParesPrimera.Add(prim);
            }
            else
            {
                throw new Exception("No hay horarios creados");
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

        public void AgregarDefecto(Hallazgo hallazgo)
        {
            var hallaz = new Hallazgo()
            {
                EmpleadoDeCalidad = hallazgo.EmpleadoDeCalidad,
                Cantidad = hallazgo.Cantidad,
                Hora = hallazgo.Hora,
                Defecto = hallazgo.Defecto,
                TipoPie = hallazgo.TipoPie
            };
            if (Horarios.Count > 0)
            {
                Horarios.LastOrDefault().Hallazgos.Add(hallazgo);
            }
            else
            {
                throw new Exception("No hay horarios creados");
            }
        }
    }
}