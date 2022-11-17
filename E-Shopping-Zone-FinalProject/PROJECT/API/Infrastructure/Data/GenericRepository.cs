using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly DataContext _dataContext;

        public GenericRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dataContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _dataContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_dataContext.Set<T>().AsQueryable(), spec);
        }

        public void Add(T entity)
        {
            _dataContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _dataContext.Set<T>().Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _dataContext.Set<T>().Remove(entity);
        }
    }
}
