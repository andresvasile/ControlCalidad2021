
using Dominio.Entities.Enums;

namespace Dominio.Entities
{
    public class Empleado : BaseEntity
    {
        public int Documento { get; set; }
        public string Nombre { get; set; }
        public string Apellido{ get; set; }
        public string Email { get; set; }
        public TipoEmpleado Rol { get; set; }
    }
}