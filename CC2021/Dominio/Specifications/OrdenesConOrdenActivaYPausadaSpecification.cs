using Dominio.Entities;
using Dominio.Entities.Enums;

namespace API.Specifications
{
    public class OrdenesConOrdenActivaYPausadaSpecification : BaseSpecification<OrdenDeProduccion>
    {
        public OrdenesConOrdenActivaYPausadaSpecification() 
            : base(o=>o.Estado == EstadoOrden.Activa || o.Estado== EstadoOrden.Pausada)
        {

        }
    }
}