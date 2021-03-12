using Dominio.Entities;

namespace API.Specifications
{
    public class ColorConNumeroSpecification : BaseSpecification<Color>
    {
        public ColorConNumeroSpecification(int codigo)
        :base(c=>c.Codigo==codigo)
        {
            
        }
    }
}