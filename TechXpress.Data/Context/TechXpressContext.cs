using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechXpress.Data.Entities;

namespace TechXpress.Data.Context
{
    public class TechXpressContext:DbContext
    {
        public TechXpressContext(DbContextOptions<TechXpressContext> options) : base(options) { }

        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<User> users { get; set; }
        public object Users { get; internal set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        

    }
}
