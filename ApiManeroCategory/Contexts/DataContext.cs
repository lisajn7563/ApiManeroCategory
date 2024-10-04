using ApiManeroCategory.Entites;
using Microsoft.EntityFrameworkCore;

namespace ApiManeroCategory.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<CategoryEntity> Category { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoryEntity>()
            .ToContainer("Categories")
            .HasPartitionKey(x => x.PartitionKey);
    }

}
