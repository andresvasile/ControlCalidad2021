using System;
using Dominio.Entities.Enums;

namespace Dominio.Entities
{
    public class Hallazgo : BaseEntity
    {
        public DateTime Hora { get; set; }
        public int Cantidad { get; set; }

        private Empleado _empleadoDeCalidad;

        public Empleado EmpleadoDeCalidad
        {
            get => _empleadoDeCalidad;
            set
            {
                if (EmpleadoDeCalidad.Rol == TipoEmpleado.SupervisorDeCalidad)
                {
                    _empleadoDeCalidad = value;
                }
            }
        }
        public TipoPie TipoPie{ get; set; }
        public Defecto Defecto{ get; set; }
        

        public Hallazgo()
        {
            
        }
    }
}