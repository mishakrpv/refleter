using Microsoft.EntityFrameworkCore;
using Storage.DataAccess.EntityConfigurations;
using Storage.Entities.Models.ScopeAggregate;

namespace Storage.DataAccess;

public sealed class StorageContext : DbContext
{
    public StorageContext(DbContextOptions<StorageContext> options) : base(options) { }
    
    public DbSet<Scope> Scopes { get; set; }
    public DbSet<ScopeIcon> Icons { get; set; }
    public DbSet<Secret> Secrets { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new ScopeEntityConfiguration());
        builder.ApplyConfiguration(new ScopeIconEntityConfiguration());
        builder.ApplyConfiguration(new SecretEntityConfiguration());
    }
}