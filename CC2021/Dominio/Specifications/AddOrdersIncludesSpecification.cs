using Dominio.Entities;

namespace API.Specifications
{
    public class AddOrdersIncludesSpecification : BaseSpecification<OrdenDeProduccion>
    {
        public AddOrdersIncludesSpecification()
        :base()
        {
            AddInclude(o=>o.Color);
            AddInclude(o=>o.Modelo);
            AddInclude(o=>o.Linea);
            AddInclude(o=>o.EmpleadoOrden);
            AddInclude(o=>o.LineaIniciadaPor);
            AddInclude(o=>o.Horarios);
        }
        
    }
}