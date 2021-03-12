using Dominio.Entities;
using Dominio.Entities.Enums;

namespace API.Specifications
{
    public class OrdenConEstadoActivoSpecification : BaseSpecification<OrdenDeProduccion>
    {
        public OrdenConEstadoActivoSpecification(LineaDeTrabajo linea,EstadoOrden estado)
        :base(o=>o.LineaId==linea.Id && o.Estado==estado)
        {
            
        }
    }
}