﻿using AccessControl.Entities.Models;
using AccessControl.Infrastructure.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Infrastructure;

public class AccessControlContext : DbContext
{
    public AccessControlContext(DbContextOptions<AccessControlContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    protected AccessControlContext() { }
    
    public virtual DbSet<AccessKey> AccessKeys { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new AccessKeyTypeConfiguration());
    }
}