using System.Linq.Expressions;
using ECommerce.Application.Repositories;
using ECommerce.Domain.Entities.Common;
using ECommerce.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Persistence.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
{
    private ECommerceDbContext _context;

    public ReadRepository(ECommerceDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();

    public IQueryable<T> GetAll(bool tracking = true)
    {
        IQueryable<T> query = Table.AsQueryable();
        if (!tracking) query = query.AsNoTracking();
        return query;
    }

    public IQueryable<T> GetWhere(Expression<Func<T, bool>> expression, bool tracking = true)
    {
        IQueryable<T> query = Table.Where(expression);
        if (!tracking) query = query.AsNoTracking();
        return query;
    }

    public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, bool tracking = true)
    {
        IQueryable<T> query = Table.AsQueryable();
        if (!tracking) query = query.AsNoTracking();
        return await query.FirstOrDefaultAsync(expression);
    }

    public async Task<T> GetByIdAsync(string id, bool tracking = true)
        // =>await Table.FirstOrDefaultAsync(c => c.Id == Guid.Parse(id));
    {
        IQueryable<T> query = Table.AsQueryable();
        if (!tracking) query = query.AsNoTracking();
        return await query.FirstOrDefaultAsync(c => c.Id == Guid.Parse(id));
    }
}