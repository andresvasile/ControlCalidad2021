using Dominio.Entities;

namespace API.Specifications
{
    public class ModeloConNumeroSpecification : BaseSpecification<Modelo>
    {
        public ModeloConNumeroSpecification(string sku)
        :base(m=>m.Sku==sku)
        {
            
        }
    }
}