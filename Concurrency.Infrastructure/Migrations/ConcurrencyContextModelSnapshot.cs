﻿// <auto-generated />
using System;
using Concurrency.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Concurrency.Infrastructure.Migrations
{
    [DbContext(typeof(ConcurrencyContext))]
    partial class ConcurrencyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("Concurrency.Domain.Models.File", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("FileType")
                        .HasColumnType("longtext");

                    b.Property<Guid?>("ObservationId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("ObservationId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("Concurrency.Domain.Models.Metadata", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("FileId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Key")
                        .HasColumnType("longtext");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("FileId");

                    b.ToTable("MetadataFile");
                });

            modelBuilder.Entity("Concurrency.Domain.Models.Observation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Observations");
                });

            modelBuilder.Entity("Concurrency.Domain.Models.File", b =>
                {
                    b.HasOne("Concurrency.Domain.Models.Observation", null)
                        .WithMany("Files")
                        .HasForeignKey("ObservationId");
                });

            modelBuilder.Entity("Concurrency.Domain.Models.Metadata", b =>
                {
                    b.HasOne("Concurrency.Domain.Models.File", null)
                        .WithMany("FileMetadata")
                        .HasForeignKey("FileId");
                });

            modelBuilder.Entity("Concurrency.Domain.Models.File", b =>
                {
                    b.Navigation("FileMetadata");
                });

            modelBuilder.Entity("Concurrency.Domain.Models.Observation", b =>
                {
                    b.Navigation("Files");
                });
#pragma warning restore 612, 618
        }
    }
}