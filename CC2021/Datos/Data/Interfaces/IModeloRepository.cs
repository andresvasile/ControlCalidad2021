using System.Collections.Generic;
using System.Threading.Tasks;
using Dominio.Entities;

namespace Datos.Data.Interfaces
{
    public interface IModeloRepository
    {
        Task<Modelo> GetModeloByIdAsync(int id);
        Task<Modelo> GetModeloBySkuAsync(string sku);
        Task<IReadOnlyList<Modelo>> GetModelosAsync();
    }
}