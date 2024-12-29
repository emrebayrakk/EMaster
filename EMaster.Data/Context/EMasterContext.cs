using EMaster.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMaster.Data.Context
{
    public class EMasterContext : DbContext
    {

        public EMasterContext(DbContextOptions<EMasterContext> options) : base(options)
        {
        }


        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries();

            foreach (var entry in entries)
            {
                if (entry.Entity is BaseEntity entity)
                {
                    var now = DateTime.UtcNow;

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entity.CreatedDate = now;
                            break;

                        case EntityState.Modified:
                            entity.UpdatedDate = now;
                            break;

                        case EntityState.Deleted:
                            entity.DeletedDate = now;
                            entry.State = EntityState.Modified;
                            break;
                    }
                }
            }

            return base.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connStr = "Data Source=DESKTOP-P87BUPQ;" +
                "Initial Catalog=EMasterAppDb;Integrated Security=True;" +
                "Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;" +
                "Application Intent=ReadWrite;Multi Subnet Failover=False";
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connStr, opt =>
                {
                    opt.EnableRetryOnFailure();
                });
            }
        }
        public DbSet<User> Users { get; set; }
    }
}
