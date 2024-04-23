﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Storage.DataAccess;

#nullable disable

namespace Storage.DataAccess.Migrations
{
    [DbContext(typeof(StorageContext))]
    partial class StorageContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Storage.Entities.Models.ScopeAggregate.Scope", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("UserId", "Name");

                    b.ToTable("Scopes");
                });

            modelBuilder.Entity("Storage.Entities.Models.ScopeAggregate.ScopeIcon", b =>
                {
                    b.Property<string>("ScopeUserId")
                        .HasColumnType("text");

                    b.Property<string>("ScopeName")
                        .HasColumnType("character varying(100)");

                    b.Property<int>("Color")
                        .HasColumnType("integer");

                    b.HasKey("ScopeUserId", "ScopeName");

                    b.ToTable("Icons");
                });

            modelBuilder.Entity("Storage.Entities.Models.ScopeAggregate.Secret", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("ScopeName")
                        .IsRequired()
                        .HasColumnType("character varying(100)");

                    b.Property<string>("ScopeUserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ScopeUserId", "ScopeName");

                    b.ToTable("Secrets");
                });

            modelBuilder.Entity("Storage.Entities.Models.ScopeAggregate.ScopeIcon", b =>
                {
                    b.HasOne("Storage.Entities.Models.ScopeAggregate.Scope", "Scope")
                        .WithOne("Icon")
                        .HasForeignKey("Storage.Entities.Models.ScopeAggregate.ScopeIcon", "ScopeUserId", "ScopeName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Scope");
                });

            modelBuilder.Entity("Storage.Entities.Models.ScopeAggregate.Secret", b =>
                {
                    b.HasOne("Storage.Entities.Models.ScopeAggregate.Scope", "Scope")
                        .WithMany("Secrets")
                        .HasForeignKey("ScopeUserId", "ScopeName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Scope");
                });

            modelBuilder.Entity("Storage.Entities.Models.ScopeAggregate.Scope", b =>
                {
                    b.Navigation("Icon")
                        .IsRequired();

                    b.Navigation("Secrets");
                });
#pragma warning restore 612, 618
        }
    }
}
