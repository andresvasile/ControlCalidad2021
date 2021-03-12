using System.Collections.Generic;
using System.Threading.Tasks;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Datos.Data
{
    public class OrdenesRepository : IOrdenesRepository
    {
        private readonly CalidadContext _context;

        public OrdenesRepository(CalidadContext context)
        {
            _context = context;
        }

        public async Task<OrdenDeProduccion> GetOrdenByIdAsync(int codigo)
        {
            return await _context.Ordenes
                .Include(o => o.Modelo)
                .Include(o => o.Color)
                .FirstOrDefaultAsync(o=>o.Id==codigo);
        }
        public async Task<OrdenDeProduccion> GetOrdenByNumeroAsync(int numero)
        {
            return await _context.Ordenes
                .Include(o => o.Modelo)
                .Include(o => o.Color)
                .FirstOrDefaultAsync(o => o.Numero == numero);
        }
        public async Task<IReadOnlyList<OrdenDeProduccion>> GetOrdenesAsync()
        {
            return await _context.Ordenes
                .Include(o=>o.Modelo)
                .Include(o=>o.Color)
                .ToListAsync();
        }
    }
}