using Dominio.Entities;

namespace API.Specifications
{
    public class OrdenConNumeroSpecification : BaseSpecification<OrdenDeProduccion>
    {
        public OrdenConNumeroSpecification(int numero)
        :base(o=>o.Numero==numero)
        {
            
        }
        
    }
}