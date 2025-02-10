using EMaster.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMaster.Infrastructure.Context
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
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var method = typeof(EMasterContext)
                        .GetMethod(nameof(ApplyGlobalFilters), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)!
                        .MakeGenericMethod(entityType.ClrType);

                    method.Invoke(null, new object[] { modelBuilder });
                }
            }

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(u => u.LastName).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
                entity.Property(u => u.PasswordHash).IsRequired();

                entity.HasOne(u => u.Company)
                      .WithMany(c => c.Users) // One company can have many users
                      .HasForeignKey(u => u.CompanyId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(50);

                entity.HasOne(c => c.Company)   
                      .WithMany()
                      .HasForeignKey(c => c.CompanyId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Income>(entity =>
            {
                entity.HasKey(i => i.Id);
                entity.Property(i => i.Amount).HasColumnType("decimal(10, 2)");

                entity.HasOne(i => i.Category)
                      .WithMany(c => c.Incomes)
                      .HasForeignKey(i => i.CategoryID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(i => i.Company)
                      .WithMany()
                      .HasForeignKey(i => i.CompanyId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Expense>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");

                entity.HasOne(e => e.Category)
                      .WithMany(c => c.Expenses)
                      .HasForeignKey(e => e.CategoryID) // Hata düzeltilmiş
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Company)
                      .WithMany()
                      .HasForeignKey(e => e.CompanyId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private static void ApplyGlobalFilters<T>(ModelBuilder modelBuilder) where T : BaseEntity
        {
            modelBuilder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connStr = "Data Source=EMRE\\SQLEXPRESS;" +
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
        public DbSet<Company> Companies { get; set; }
    }
}
