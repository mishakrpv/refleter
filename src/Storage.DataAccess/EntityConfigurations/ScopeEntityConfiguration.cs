using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Entities.Models.ScopeAggregate;

namespace Storage.DataAccess.EntityConfigurations;

public sealed class ScopeEntityConfiguration : IEntityTypeConfiguration<Scope>
{
    public void Configure(EntityTypeBuilder<Scope> builder)
    {
        builder.HasKey(s => s.Id);

        builder.HasOne(s => s.Icon)
            .WithOne(si => si.Scope)
            .HasForeignKey<ScopeIcon>(si => si.Id)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasMany(s => s.Secrets)
            .WithOne(s => s.Scope)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasIndex(s => new { s.UserId, s.Name }, "IX_Scopes_UserId_Name_key")
            .IsUnique();
        
        builder.Property(s => s.Name)
            .HasMaxLength(100);
    }
}