using AccessControl.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccessControl.Infrastructure.EntityTypeConfigurations;

public sealed class AccessKeyTypeConfiguration : IEntityTypeConfiguration<AccessKey>
{
    public void Configure(EntityTypeBuilder<AccessKey> builder)
    {
        builder.HasKey(ak => ak.Id);
    }
}