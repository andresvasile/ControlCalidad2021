using Dominio.Entities;

namespace API.Specifications
{
    public class DefectoDescripcionSpecification : BaseSpecification<Defecto>
    {
        public DefectoDescripcionSpecification(string descripcionDefecto)
        :base(d=>d.Descripcion==descripcionDefecto)
        {
            
        }
    }
}