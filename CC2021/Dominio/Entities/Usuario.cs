using Dominio.Entities.Enums;

namespace Dominio.Entities
{
    public class Usuario : BaseEntity
    {
        public string User{ get; set; }
        public string Password{ get; set; }
        public string DisplayName { get; set; }
        public Empleado Empleado { get; set; }
    }
}
