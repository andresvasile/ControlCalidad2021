using System;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using Dominio.Entities;

namespace API.Specifications
{
    public class LineaConIdSpecification : BaseSpecification<LineaDeTrabajo>
    {
        public LineaConIdSpecification(int numeroLinea) 
            :base(l=>l.Id==numeroLinea)
        {
            AddInclude(x=>x.OrdenesDeProduccion);
            AddInclude(x=>x.Empleado);
        }
    }
}