using Dominio.Entities;
using Dominio.Entities.Enums;

namespace API.Specifications
{
    public class OrdenesConOrdenActivaYPausadaSpecification : BaseSpecification<OrdenDeProduccion>
    {
        public OrdenesConOrdenActivaYPausadaSpecification() 
            : base(o=>o.Estado == EstadoOrden.Activa || o.Estado== EstadoOrden.Pausada)
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