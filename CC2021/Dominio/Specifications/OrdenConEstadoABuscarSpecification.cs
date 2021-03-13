using Dominio.Entities;
using Dominio.Entities.Enums;

namespace API.Specifications
{
    public class OrdenConEstadoABuscarSpecification : BaseSpecification<OrdenDeProduccion>
    {
        public OrdenConEstadoABuscarSpecification(LineaDeTrabajo linea,EstadoOrden estado)
        :base(o=>o.LineaId==linea.Id && o.Estado==estado)
        {
            AddInclude(o => o.Color);
            AddInclude(o => o.Modelo);
            AddInclude(o => o.Linea);
            AddInclude(o => o.EmpleadoOrden);
            AddInclude(o => o.LineaIniciadaPor);
            AddInclude(o=>o.Horarios);
        }
    }
}