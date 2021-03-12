using Dominio.Entities;

namespace API.Specifications
{
    public class ValidarEmpleadoSpecification : BaseSpecification<Usuario>
    {
        public ValidarEmpleadoSpecification(string user)
        :base(u => u.User==user)
        {
            
        }
    }
}