using Dominio.Entities;
using Dominio.Entities.Enums;

namespace API.Specifications
{
    public class OrdenesConOrdenActivaYPausadaConLineaSpecification : BaseSpecification<OrdenDeProduccion>
    {
        public OrdenesConOrdenActivaYPausadaConLineaSpecification(LineaDeTrabajo linea)
        :base(x=>x.Linea==linea && (x.Estado==EstadoOrden.Activa || x.Estado==EstadoOrden.Pausada))        
        {
            AddInclude(o => o.Color);
            AddInclude(o => o.Modelo);
            AddInclude(o => o.Linea);
            AddInclude(o => o.EmpleadoOrden);
            AddInclude(o => o.LineaIniciadaPor);
            AddInclude(o => o.Horarios);
        }
        
    }
}