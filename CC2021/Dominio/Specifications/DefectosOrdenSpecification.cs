using Dominio.Entities;

namespace API.Specifications
{
    public class DefectosOrdenSpecification : BaseSpecification<Hallazgo>

    {
        public DefectosOrdenSpecification(int id)
        :base(x=>x.HorarioTrabajoId==id)
        {
            AddInclude(x=>x.Defecto);
            AddInclude(x=>x.EmpleadoDeCalidad);
        }   
    }
}