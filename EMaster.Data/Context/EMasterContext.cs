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
                            entity.IsDeleted = false;
                            break;

                        case EntityState.Modified:
                            entity.UpdatedDate = now;
                            entity.IsDeleted = false;
                            break;

                        case EntityState.Deleted:
                            entity.DeletedDate = now;
                            entity.IsDeleted = true;
                            entry.State = EntityState.Modified;
                            break;
                    }
                }
            }

            return base.SaveChanges();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<Income>(entity =>
            {
                entity.HasKey(i => i.Id);
                entity.Property(i => i.Amount).HasColumnType("decimal(10, 2)");
                entity.HasOne(i => i.Category)
                      .WithMany(c => c.Incomes)
                      .HasForeignKey(i => i.CategoryID);
            });

            modelBuilder.Entity<Expense>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
                entity.HasOne(e => e.Category)
                      .WithMany(c => c.Expenses)
                      .HasForeignKey(e => e.CategoryID);
            });
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
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Income> Incomes { get; set; } = null!;
        public DbSet<Expense> Expenses { get; set; } = null!;
        public DbSet<User> Users { get; set; }
    }
}
