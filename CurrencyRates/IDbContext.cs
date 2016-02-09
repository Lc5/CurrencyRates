using System.Data.Entity;

namespace CurrencyRates
{
    public interface IDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        int SaveChanges();
    }
}
