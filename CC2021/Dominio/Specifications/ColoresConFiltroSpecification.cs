using Dominio.Entities;

namespace API.Specifications
{
    public class ColoresConFiltroSpecification : BaseSpecification<Color>
    {
        public ColoresConFiltroSpecification(string spec)
            : base(c=>c.Descripcion.Contains(spec))
        {

        }
    }
}