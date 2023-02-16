using Ardalis.Specification.EntityFrameworkCore;
using Promomash.Core.Interfaces;

namespace Promomash.Infrastructure;

public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
    public EfRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
