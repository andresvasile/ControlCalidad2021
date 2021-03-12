using Dominio.Entities;

namespace API.Specifications
{
    public class OrdenConColoresYModelosSpecification : BaseSpecification<OrdenDeProduccion>
    {
        public OrdenConColoresYModelosSpecification()
        {
            AddInclude(x=>x.Modelo);
            AddInclude(x =>x.Color);
        }

        public OrdenConColoresYModelosSpecification(int numero)
        : base(x=>x.Numero == numero)
        {
            AddInclude(x => x.Modelo);
            AddInclude(x => x.Color);
        }
    }
}