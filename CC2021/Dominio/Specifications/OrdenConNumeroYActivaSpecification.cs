using System.Security.Cryptography.X509Certificates;
using Dominio.Entities;
using Dominio.Entities.Enums;

namespace API.Specifications
{
    public class OrdenConNumeroYActivaSpecification : BaseSpecification<OrdenDeProduccion>
    {
        public OrdenConNumeroYActivaSpecification(int numero)
            : base(o => o.Numero == numero && o.Estado==EstadoOrden.Activa)
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