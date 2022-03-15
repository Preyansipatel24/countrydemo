using Microsoft.EntityFrameworkCore;

namespace CountryDemo.Models
{
    public class CountryContext : DbContext
    {
        public CountryContext()
        {

        }
       public CountryContext(DbContextOptions<CountryContext> options): base(options)
        {

        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<State> StateData { get; set; }
        public DbSet<City> Cities { get; set; }


    }
}
