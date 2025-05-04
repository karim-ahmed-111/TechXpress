using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TechXpress.Domain.Entities;

namespace TechXpress.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // DbSets for domain entities
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        // Optional: seed initial categories/products in OnModelCreating via HasData (or do in code)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Example: Seed roles or entities if desired
            // (Alternatively, seed roles and sample data in code after DB creation)

            // Sample data seeding (optional):
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Electronics" },
                new Category { CategoryId = 2, Name = "Books" }
            );
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Name = "Laptop",Description="New Laptop", CategoryId = 1, Price = 999.99M },
                new Product { ProductId = 2, Name = "Novel", CategoryId = 2,Description="New Novel", Price = 19.99M }
            );
        }
    }
}
