using CurrencyRates.Entity;
using System.Data.Entity;

namespace CurrencyRates
{
    public class Context : DbContext
    {
        public Context() : base() {}

        public DbSet<Currency> Currencies { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Rate> Rates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rate>().Property(e => e.Value).HasPrecision(8, 4);

            base.OnModelCreating(modelBuilder);
        }
    }
}
