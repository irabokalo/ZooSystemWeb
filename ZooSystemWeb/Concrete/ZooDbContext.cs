using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using Zoo_system.Entities;

namespace Zoo_system.Concrete
{
    public class ZooDbContext : DbContext
    {
        public ZooDbContext() : base("ZooDb")
        {
        }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Zoo> Zoos { get; set; }
        public DbSet<Worker> Workers { get; set; }
    }
}
