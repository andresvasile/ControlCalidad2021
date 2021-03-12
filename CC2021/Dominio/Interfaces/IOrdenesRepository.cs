using System.Collections.Generic;
using System.Threading.Tasks;
using Dominio.Entities;

namespace Dominio.Interfaces
{
    public interface IOrdenesRepository
    {
        Task<OrdenDeProduccion> GetOrdenByIdAsync(int codigo);
        public Task<OrdenDeProduccion> GetOrdenByNumeroAsync(int numero);
        Task<IReadOnlyList<OrdenDeProduccion>> GetOrdenesAsync();
    }
}