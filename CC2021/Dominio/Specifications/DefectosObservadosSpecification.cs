using Dominio.Entities;
using Dominio.Entities.Enums;

namespace API.Specifications
{
    public class DefectosObservadosSpecification : BaseSpecification<Defecto>
    {
        public DefectosObservadosSpecification(TipoDefecto tipo) 
            : base(d=>d.TipoDefecto==tipo)
        {
            
        }
    }
}