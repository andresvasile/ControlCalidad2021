using System.Security.Cryptography.X509Certificates;
using Dominio.Entities;

namespace API.Specifications
{
    public class OrdenConEmpleadoAsociadoSpecification : BaseSpecification<OrdenDeProduccion>
    {
        public OrdenConEmpleadoAsociadoSpecification(int numeroOrden, Empleado empleado)
        :base(o=>o.Numero==numeroOrden && o.EmpleadoOrden==empleado)
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