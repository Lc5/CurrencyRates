namespace CurrencyRates.Model
{
    using System.Data.Entity;

    using CurrencyRates.Model.Entity;

    public class Context : DbContext
    {
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
