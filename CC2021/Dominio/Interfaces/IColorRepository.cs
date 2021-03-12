using System.Collections.Generic;
using System.Threading.Tasks;
using Dominio.Entities;

namespace Dominio.Interfaces
{
    public interface IColorRepository
    {
        Task<Color> GetColorByIdAsync(int id);
        Task<Color> GetColorByCodigo(int codigo);
        Task<IReadOnlyList<Color>> GetColoresAsync();
    }
}