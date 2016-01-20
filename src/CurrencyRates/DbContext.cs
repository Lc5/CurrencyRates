using CurrencyRates.Entity;
using System.Data.Entity;

namespace CurrencyRates
{
    class Context : DbContext
    {
        public Context() : base() {}

        public DbSet<Currency> Currencies { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Rate> Rates { get; set; }
    }
}
