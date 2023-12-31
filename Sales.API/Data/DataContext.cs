using Microsoft.EntityFrameworkCore;
using Sales.Share.Entities;

namespace Sales.API.Data
{
	public class DataContext : DbContext
	{
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Country> Countries { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			//Le estamos indicando la definición el modelo para crearlo en la bd, donde el indicé unico será el nombre
			modelBuilder.Entity<Country>().HasIndex(a => a.Name).IsUnique();
		}
	}
}
