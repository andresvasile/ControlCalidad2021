using Dominio.Entities;

namespace API.Specifications
{
    public class LineaConNumeroYEmpleadoSpecification : BaseSpecification<LineaDeTrabajo>
    {
        public LineaConNumeroYEmpleadoSpecification(int numeroOrden, Empleado empleado)
        :base(x=>x.Numero==numeroOrden && x.Empleado==empleado)
        {
            AddInclude(x=>x.OrdenesDeProduccion);
            AddInclude(x=>x.Empleado);
        }
    }
}