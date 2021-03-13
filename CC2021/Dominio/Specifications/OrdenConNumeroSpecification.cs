using System.Linq;
using Dominio.Entities;

namespace API.Specifications
{
    public class OrdenConNumeroSpecification : BaseSpecification<OrdenDeProduccion>
    {
        public OrdenConNumeroSpecification(int numero)
            : base(o => o.Numero == numero)
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