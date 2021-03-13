using Dominio.Entities;
using Dominio.Entities.Enums;

namespace API.Specifications
{
    public class OrdenConEstadoActivaOPausadaYNumero : BaseSpecification<OrdenDeProduccion>
    {
        public OrdenConEstadoActivaOPausadaYNumero(int numeroOrden)
        :base(x=>(x.Numero==numeroOrden && x.Estado==EstadoOrden.Activa) || (x.Numero == numeroOrden && x.Estado==EstadoOrden.Pausada))
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