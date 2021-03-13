using Dominio.Entities;

namespace API.Specifications
{
    public class AddLineasIncludesSpecification : BaseSpecification<LineaDeTrabajo>

    {
        public AddLineasIncludesSpecification()
        :base()
        {
            AddInclude(x=>x.Empleado);
            AddInclude(x=>x.OrdenesDeProduccion);
        }
    }
}