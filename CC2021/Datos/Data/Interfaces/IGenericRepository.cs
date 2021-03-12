using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dominio.Entities;
using Dominio.Interfaces;

namespace Datos.Data.Interfaces
{
    public interface IGenericRepository<T>
    where T: BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<List<T>> ListAllAsync();
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<List<T>> ListAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}