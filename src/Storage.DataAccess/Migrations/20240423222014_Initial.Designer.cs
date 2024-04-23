﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Storage.DataAccess;

#nullable disable

namespace Storage.DataAccess.Migrations
{
    [DbContext(typeof(StorageContext))]
    [Migration("20240423222014_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Storage.Entities.Models.ScopeAggregate.Scope", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "UserId", "Name" }, "IX_Scopes_UserId_Name_key")
                        .IsUnique();

                    b.ToTable("Scopes");
                });

            modelBuilder.Entity("Storage.Entities.Models.ScopeAggregate.ScopeIcon", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("Color")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Icons");
                });

            modelBuilder.Entity("Storage.Entities.Models.ScopeAggregate.Secret", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("ScopeId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ScopeId");

                    b.ToTable("Secrets");
                });

            modelBuilder.Entity("Storage.Entities.Models.ScopeAggregate.ScopeIcon", b =>
                {
                    b.HasOne("Storage.Entities.Models.ScopeAggregate.Scope", "Scope")
                        .WithOne("Icon")
                        .HasForeignKey("Storage.Entities.Models.ScopeAggregate.ScopeIcon", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Scope");
                });

            modelBuilder.Entity("Storage.Entities.Models.ScopeAggregate.Secret", b =>
                {
                    b.HasOne("Storage.Entities.Models.ScopeAggregate.Scope", "Scope")
                        .WithMany("Secrets")
                        .HasForeignKey("ScopeId")
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