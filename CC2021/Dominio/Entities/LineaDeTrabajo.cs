using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Dominio.Entities.Enums;

namespace Dominio.Entities
{
    public class LineaDeTrabajo : BaseEntity
    {
        public int Numero { get; set; }
        public virtual List<OrdenDeProduccion> OrdenesDeProduccion { get; set; }
        private Empleado _empleado { get; set; }

        public Empleado Empleado
        {
            get;
            set;
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

        public OrdenDeProduccion CrearOrden(int numeroOrden, Modelo modelo, Color color, Empleado empleado
            , List<Turno> turnos, in DateTime horaActual)
        {
            var turnoValidado = ValidarTurno(turnos, horaActual);
            if (turnoValidado != null)
            {
                Empleado = empleado;
                var orden = new OrdenDeProduccion
                    (numeroOrden, color, modelo, turnoValidado, this, horaActual);

                this.OrdenesDeProduccion.Add(orden);
                return orden;
            }

            return null;
        }

        public Turno ValidarTurno(List<Turno> turnos, in DateTime horaActual)
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
            var o = OrdenesDeProduccion.SingleOrDefault(o => o.Numero == orden.Numero);
            if (o != null)
            {
                o.PausarOrden(horaActual);
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

        public bool ValidarSupervisor(Empleado empleado)
        {

            var oActiva = OrdenesDeProduccion
                .Any(o =>
                (o.Estado == EstadoOrden.Activa && o.EmpleadoOrden == null));

            var oPausada = OrdenesDeProduccion
                .Any(o =>
                (o.Estado == EstadoOrden.Pausada && o.EmpleadoOrden != empleado));

            if (oActiva || oPausada)
            {
                return true;
            }
            return false;
        }

        public bool ComprobarAsignacionSupervisor(Empleado empleado)
        {
            var siEstanFinalizadas = ObtenerLineaSiEstaFinalizada(EstadoOrden.Finalizada);

            var siEstaAsignadoAActivaOPausada = ObtenerSiEstaActivaOPausada(EstadoOrden.Pausada,EstadoOrden.Activa);

            if (Empleado != null)
            {
                if (Empleado.Id == empleado.Id)
                {
                    if (siEstanFinalizadas && !siEstaAsignadoAActivaOPausada)
                    {
                        return true;
                    }
                    return false;
                }
                return true;
            }
            return true;
        }

        private bool ObtenerSiEstaActivaOPausada(EstadoOrden pausada, EstadoOrden activa)
        {
            if (OrdenesDeProduccion.Count == 0)
            {
                return true;
            }

            if (OrdenesDeProduccion.Count > 0)
            {
                return OrdenesDeProduccion.Any(x => x.Estado == pausada || x.Estado == activa);
            }

            return false;
        }
    }
}