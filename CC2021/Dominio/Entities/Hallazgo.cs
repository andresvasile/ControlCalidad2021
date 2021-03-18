using System;
using Dominio.Entities.Enums;

namespace Dominio.Entities
{
    public class Hallazgo : BaseEntity
    {
        public DateTime Hora { get; set; }
        public int Cantidad { get; set; }
        private Empleado _empleadoDeCalidad;

        public virtual Empleado EmpleadoDeCalidad
        {
            get;
            set;
        }
        public TipoPie TipoPie{ get; set; }
        public virtual Defecto Defecto{ get; set; }
        public int HorarioTrabajoId{ get; set; }

        public Hallazgo()
        {
            
        }
    }
}