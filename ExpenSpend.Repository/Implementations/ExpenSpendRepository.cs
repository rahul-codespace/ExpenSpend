using ExpenSpend.Data.Context;
using ExpenSpend.Domain;
using Microsoft.EntityFrameworkCore;

namespace ExpenSpend.Data.Repository;

public class ExpenSpendRepository<TEntity> : IExpenSpendRepository<TEntity> where TEntity : class
{
    private readonly ExpenSpendDbContext _context;

    public ExpenSpendRepository(ExpenSpendDbContext context)
    {
        _context = context;
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(Guid id)
    {
#pragma warning disable CS8603 // Possible null reference return.
        return await _context.Set<TEntity>().FindAsync(id);
#pragma warning restore CS8603 // Possible null reference return.
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null)
            return false;

        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
