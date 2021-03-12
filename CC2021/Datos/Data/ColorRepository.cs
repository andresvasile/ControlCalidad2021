using System.Collections.Generic;
using System.Threading.Tasks;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Datos.Data
{
    public class ColorRepository : IColorRepository
    {
        private readonly CalidadContext _contexto;

        public ColorRepository(CalidadContext contexto)
        {
            _contexto = contexto;
        }
        public async Task<Color> GetColorByIdAsync(int id)
        { 
            return await _contexto.Colores.FindAsync(id);
        }
        public async Task<Color> GetColorByCodigo(int codigo)
        {
            return await _contexto.Colores.FirstOrDefaultAsync(c => c.Codigo == codigo);
        }
        public async Task<IReadOnlyList<Color>> GetColoresAsync()
        {
            return await _contexto.Colores.ToListAsync();
        }
    }
}