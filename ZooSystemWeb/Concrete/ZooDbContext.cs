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
            Database.SetInitializer(new ZooDbContextInitializer());
        }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Zoo> Zoos { get; set; }
        public DbSet<Worker> Workers { get; set; }

        public class ZooDbContextInitializer : DropCreateDatabaseIfModelChanges<ZooDbContext>
        {
            protected override void Seed(ZooDbContext context)
            {
                Zoo firstZoo = new Zoo
                {
                    Name = "Happy animals"
                };
                context.Zoos.Add(firstZoo);
                context.Zoos.Add(new Zoo
                {
                    Name = "Lovely zoo"
                });
                context.Zoos.Add(new Zoo
                {
                    Name = "the Best zoo"
                });
                context.SaveChanges();
                base.Seed(context);
            }
        }
    }
}
