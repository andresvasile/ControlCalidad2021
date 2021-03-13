using System;
using Dominio.Entities.Enums;

namespace Dominio.Entities
{
    public class Primera : BaseEntity
    {
        public DateTime Hora { get; set; }
        public int Cantidad { get; set; }

        private  Empleado _empleadoDeCalidad;

        public virtual Empleado EmpleadoDeCalidad
        {
            get;
            set;
        }

        public Primera()
        {
            
        }
    }
}