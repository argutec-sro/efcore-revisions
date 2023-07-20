﻿// <auto-generated />
using System;
using Example.Simple;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Example.Simple.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230720120958_BatchColumns")]
    partial class BatchColumns
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Argutec.EfCore.Revisions.Revision", b =>
                {
                    b.Property<Guid>("ID")
                        .HasColumnType("uuid");

                    b.Property<Guid>("BatchID")
                        .HasColumnType("uuid");

                    b.Property<int>("BatchOperation")
                        .HasColumnType("integer");

                    b.Property<string>("BatchTables")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)");

                    b.Property<string>("Column")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Current")
                        .HasColumnType("text");

                    b.Property<string>("Original")
                        .HasColumnType("text");

                    b.Property<string>("RecordID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Table")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("User")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("ID");

                    b.ToTable("Revisions");
                });

            modelBuilder.Entity("Example.Simple.Book", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTime>("ReleaseDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTime(2023, 7, 20, 12, 9, 58, 481, DateTimeKind.Utc).AddTicks(2074));

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.ToTable("Books");
                });
#pragma warning restore 612, 618
        }
    }
}