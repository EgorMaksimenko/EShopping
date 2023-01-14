using EShopping.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Core.Persistance
{
    public class EShoppingTutorialDbContext : DbContext
    {
        public virtual DbSet<Order> Orders { get; set; }

        public EShoppingTutorialDbContext
        (DbContextOptions<EShoppingTutorialDbContext> dbContextOptions)
            : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Scans a given assembly for all types that implement 
            //IEntityTypeConfiguration, and registers each one automatically
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
