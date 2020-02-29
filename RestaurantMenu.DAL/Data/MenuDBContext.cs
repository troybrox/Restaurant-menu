using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Design;
using RestaurantMenu.DAL.Entities;

namespace RestaurantMenu.DAL.Data
{
    public class MenuDBContext : DbContext
    {
        public MenuDBContext(DbContextOptions options) : base(options) { }

        public DbSet<Dish> Dishes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dish>().HasAlternateKey(d => d.Name);
        }

        public class EFDBContextFactory : IDesignTimeDbContextFactory<MenuDBContext>
        {
            MenuDBContext IDesignTimeDbContextFactory<MenuDBContext>.CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder();
                optionsBuilder.UseSqlServer(@"Server = localhost\SQLEXPRESS; Database = Restaurant; Trusted_Connection = True;");
                return new MenuDBContext(optionsBuilder.Options);
            }
        }
    }
}
