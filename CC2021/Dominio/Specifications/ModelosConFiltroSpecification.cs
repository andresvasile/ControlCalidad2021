using Dominio.Entities;

namespace API.Specifications
{
    public class ModelosConFiltroSpecification : BaseSpecification<Modelo>
    {
        public ModelosConFiltroSpecification(string sku)
        :base(m=>m.Denominacion==sku)
        {
            
        }
        
    }
}