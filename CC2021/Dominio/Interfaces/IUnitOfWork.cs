using System;
using System.Threading.Tasks;
using Dominio.Entities;
using Dominio.Interfaces;

namespace Dominio.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>()
            where TEntity : BaseEntity;

        Task<int> Complete();

        DateTime GetHora();

    }
}