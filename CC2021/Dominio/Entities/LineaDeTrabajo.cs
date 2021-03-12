using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities.Enums;

namespace Dominio.Entities
{
    public class LineaDeTrabajo : BaseEntity
    {
        public int Numero { get; set; }
        public List<OrdenDeProduccion> OrdenesDeProduccion { get; set; }
        private Empleado _empleado { get; set; }

        public Empleado Empleado
        {
            get => _empleado;
            set
            {
                if (Empleado.Rol == TipoEmpleado.SupervisorDeLinea)
                {
                    _empleado = value;
                }
            }
        }

        public LineaDeTrabajo()
        {
            if (OrdenesDeProduccion == null) OrdenesDeProduccion = new List<OrdenDeProduccion>();
        }

        public bool ObtenerLineaSiEstaFinalizada(EstadoOrden estadoFinalizada)
        {
            if (OrdenesDeProduccion.Count == 0)
            {
                return true;
            }
            if (OrdenesDeProduccion.Count > 0)
            {
                if (OrdenesDeProduccion.All(e => e.Estado == estadoFinalizada))
                {
                    return true;
                }
            }

            return false;
        }

        public bool CrearOrden(int numeroOrden, Modelo modelo, Color color, Empleado empleado
            , List<Turno> turnos, in DateTime horaActual)
        {
            var turnoValidado = ValidarTurno(turnos, horaActual);
            if (turnoValidado != null)
            {
                Empleado = empleado;
                var orden = new OrdenDeProduccion
                    (numeroOrden, color, modelo, turnoValidado, this, horaActual);

                this.OrdenesDeProduccion.Add(orden);
                return true;
            }

            return false;
        }

        private Turno ValidarTurno(List<Turno> turnos, in DateTime horaActual)
        {
            foreach (var turno in turnos)
            {
                var turnoValido = turno.EsTurnoValido(horaActual);

                if (turnoValido) return turno;

            }

            return null;
        }

        public void PausarOrden(DateTime horaActual, OrdenDeProduccion orden)
        {
            foreach (var o in OrdenesDeProduccion)
            {
                if (o.Numero == orden.Numero)
                {
                    o.PausarOrden(horaActual);
                }
            }

        }

        public void ReanudarOrden(List<Turno> turnos, in DateTime horaActual, OrdenDeProduccion orden)
        {


            foreach (var o in OrdenesDeProduccion)
            {
                if (o.Numero == orden.Numero)
                {
                    var turnoValidado = ValidarTurno(turnos, horaActual);
                    if (turnoValidado != null)
                    {
                        o.ReanudarOrden(turnoValidado, horaActual);
                    }
                }
            }

        }

        public void FinalizarOrden(in DateTime horaActual, OrdenDeProduccion orden)
        {
            foreach (var o in OrdenesDeProduccion)
            {
                if (o.Numero == orden.Numero)
                {
                    o.FinalizarOrden(horaActual);
                }
            }
        }

        public OrdenDeProduccion ValidarSupervisor(OrdenDeProduccion orden, Empleado empleado)
        {
            var ordenSupervisada = OrdenesDeProduccion.FirstOrDefault(x => (x.EmpleadoOrden == empleado && x.Estado == EstadoOrden.Activa) ||
                                          (x.EmpleadoOrden == empleado && x.Estado == EstadoOrden.Pausada));

            if (ordenSupervisada == null)
            {
                foreach(var o in OrdenesDeProduccion)
                {
                    if (o.Numero == orden.Numero)
                    {
                        o.AsociarSupervisor(empleado);

                        return o;
                    }
                }
                
            }
            return null;
        }

        public OrdenDeProduccion DesasociarSupervisor(OrdenDeProduccion orden, Empleado empleado)
        {
            var ordenParaDesasociar = OrdenesDeProduccion
                .FirstOrDefault(x => x.Estado == EstadoOrden.Pausada && x.Numero == orden.Numero
                && x.EmpleadoOrden==empleado);

            if (ordenParaDesasociar != null)
            {
                foreach (var o in OrdenesDeProduccion)
                {
                    if (o.Numero == orden.Numero)
                    {
                        o.DesasociarSupervisor(empleado);

                        return o;
                    }
                }
            }

            return null;
        }
    }
}