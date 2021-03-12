using System;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using Dominio.Entities;

namespace API.Specifications
{
    public class LineaConNumeroSpecification : BaseSpecification<LineaDeTrabajo>
    {
        public LineaConNumeroSpecification(int numeroLinea) 
            :base(l=>l.Numero==numeroLinea)
        {
            
        }
    }
}