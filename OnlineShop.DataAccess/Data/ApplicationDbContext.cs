using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;

namespace OnlineShop.DataAccess.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Category>().HasData(
            //    new Category { Id = 1, Name = "Action", DisplayOrder = 1 }
            //    );

            modelBuilder.Entity<Product>().HasData(
               new Product { Id = 1, Title = "Fortune of Time", Author = "Bully Spark", Description = "test", ISBN = "SWD99901", ListPrice = 99, Price = 90, Price100 = 80, Price50 = 85, CategoryId = 5, ImageUrl = "" }
               );
        }
    }
}
