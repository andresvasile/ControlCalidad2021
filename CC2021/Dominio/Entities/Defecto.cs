using Dominio.Entities.Enums;

namespace Dominio.Entities
{
    public class Defecto : BaseEntity
    {
        public TipoDefecto TipoDefecto { get; set; }
        public string Descripcion { get; set; }

    }
}