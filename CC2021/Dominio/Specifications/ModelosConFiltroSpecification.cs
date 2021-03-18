using Dominio.Entities;

namespace API.Specifications
{
    public class ModelosConFiltroSpecification : BaseSpecification<Modelo>
    {
        public ModelosConFiltroSpecification(string filtro)
        :base(m=>m.Denominacion.Contains(filtro))
        {
            
        }
        
    }
}