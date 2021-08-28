using EventsPlanner.DomainContext.DomainEntities;
using EventsPlanner.DomainContext.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsPlanner.DomainContext.DataContext
{
    public class DomainDataContext : DbContext
    {
        public DomainDataContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        public DbSet<SubcategoryEvent> SubcategoryEvent { get; set; }
        public DbSet<CategoryEvent> CategoryEvent { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
    }
}
