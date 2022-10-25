using FINSTAR_Test_Task.Infrastructure.DAO;
using Microsoft.EntityFrameworkCore;

namespace FINSTAR_Test_Task.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<CodeValueEntity>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<CodeValueEntity>()
            .Property(x => x.Code)
            .IsRequired();
        modelBuilder.Entity<CodeValueEntity>()
            .Property(x => x.Value)
            .IsRequired();
    }
    
    public virtual DbSet<CodeValueEntity> CodeValue { get; set; }
}