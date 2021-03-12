using Dominio.Entities;

namespace API.Specifications
{
    public class ValidarUsuarioSpecification : BaseSpecification<Usuario>
    {
        public ValidarUsuarioSpecification(string usuario, string password)
        :base(u=>u.User==usuario && u.Password==password)
        {
            AddInclude(x=>x.Empleado);
        }        
    }
}