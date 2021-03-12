using System.Collections.Generic;
using System.Threading.Tasks;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Datos.Data
{
    public class ModeloRepository : IModeloRepository
    {
        private readonly CalidadContext _contexto;

        public ModeloRepository(CalidadContext contexto)
        {
            _contexto = contexto;
        }
        public async Task<Modelo> GetModeloByIdAsync(int id)
        {
            return await _contexto.Modelos.FindAsync(id);
        }

        public async Task<Modelo> GetModeloBySkuAsync(string sku)
        {
            return await _contexto.Modelos.FirstOrDefaultAsync(s=>s.Sku==sku);
        }

        public async Task<IReadOnlyList<Modelo>> GetModelosAsync()
        {
            return await _contexto.Modelos.ToListAsync();
        }
    }
}