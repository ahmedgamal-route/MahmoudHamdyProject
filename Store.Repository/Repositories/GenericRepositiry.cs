using Microsoft.EntityFrameworkCore;
using Store.Data.Context;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using Store.Repository.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Repositories
{
    public class GenericRepositiry<TEntity, TKey> : IgenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _context;

        public GenericRepositiry(StoreDbContext context) 
        {
            _context = context;
        }

        public async Task AddAsync(TEntity entity)
        => await _context.Set<TEntity>().AddAsync(entity);

        public void Delete(TEntity entity)
          => _context.Set<TEntity>().Remove(entity);

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
                => await _context.Set<TEntity>().ToListAsync();

        public async Task<IReadOnlyList<TEntity>> GetAllAsNoTrackingAsync()
                => await _context.Set<TEntity>().ToListAsync();

        public async Task<TEntity> GetByIdAsync(TKey? Id)
              => await _context.Set<TEntity>().FindAsync(Id);

        public void Update(TEntity entity)
               => _context.Set<TEntity>().Update(entity);

        public async Task<TEntity> GetWithSpecificationByIdAsync(ISepcification<TEntity> specs)
        => await ApplySpecification(specs).FirstOrDefaultAsync();

        public async Task<IReadOnlyList<TEntity>> GetAllWithSpecificationAsync(ISepcification<TEntity> specs)
          => await ApplySpecification(specs).ToListAsync();

        private IQueryable<TEntity> ApplySpecification(ISepcification<TEntity> specs)
            => SpecificationEvaluator<TEntity, TKey>.GetQuery(_context.Set<TEntity>(), specs);

        public async Task<int> GetCountSpecificationAsync(ISepcification<TEntity> specs)
            => await ApplySpecification(specs).CountAsync();
    }
}
