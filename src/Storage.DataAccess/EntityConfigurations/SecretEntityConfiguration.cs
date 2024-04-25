using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Entities.Models.ScopeAggregate;

namespace Storage.DataAccess.EntityConfigurations;

public sealed class SecretEntityConfiguration : IEntityTypeConfiguration<Secret>
{
    public void Configure(EntityTypeBuilder<Secret> builder)
    {
        builder.HasKey(s => s.Id);
        
        builder.HasIndex(s => new { s.ScopeId, s.Name }, name: "IX_Secrets_ScopeId_Name_key")
            .IsUnique();
        
        builder.Property(s => s.Name)
            .HasMaxLength(100);
    }
}