using Dominio.Entities;

namespace API.Specifications
{
    public class HorariosDeOrdenSpecification : BaseSpecification<HorarioTrabajo>
    {
        public HorariosDeOrdenSpecification(int ordenId)
        :base(x=>x.OrdenDeProduccionId== ordenId)
        {
            AddInclude(x=>x.Hallazgos);
            AddInclude(x=>x.ParesPrimera);
            AddInclude(x=>x.Hermanado);
            AddInclude(x=>x.Turno);
            AddInclude(x=>x.OrdenDeProduccion);
        }
        
    }
}