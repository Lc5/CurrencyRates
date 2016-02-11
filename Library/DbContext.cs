using CurrencyRates.Library.Entity;
using System.Data.Entity;

namespace CurrencyRates
{
    public class Context : DbContext
    {
        public Context() : base() {}

        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<Rate> Rates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rate>().Property(e => e.Value).HasPrecision(8, 4);

            base.OnModelCreating(modelBuilder);
        }
    }
}
