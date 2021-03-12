using System.Security.Cryptography.X509Certificates;
using Dominio.Entities;

namespace API.Specifications
{
    public class OrdenConEmpleadoAsociadoSpecification : BaseSpecification<OrdenDeProduccion>
    {
        public OrdenConEmpleadoAsociadoSpecification(int numeroOrden, Empleado empleado)
        :base(o=>o.Numero==numeroOrden && o.EmpleadoOrden==empleado)
        {
            
        }
        
    }
}