using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Entities.Models.ScopeAggregate;

namespace Storage.DataAccess.EntityConfigurations;

public sealed class ScopeIconEntityConfiguration : IEntityTypeConfiguration<ScopeIcon>
{
    public void Configure(EntityTypeBuilder<ScopeIcon> builder)
    {
        builder.HasKey(si => new { si.ScopeUserId, si.ScopeName });
    }
}