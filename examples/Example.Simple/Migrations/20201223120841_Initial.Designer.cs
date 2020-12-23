﻿// <auto-generated />
using System;
using Example.Simple;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Example.Simple.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20201223120841_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Argutec.EfCore.Revisions.Revision", b =>
                {
                    b.Property<Guid>("ID")
                        .HasColumnType("uuid");

                    b.Property<Guid>("BatchID")
                        .HasColumnType("uuid");

                    b.Property<string>("Column")
                        .IsRequired()
                        .HasColumnType("character varying(128)")
                        .HasMaxLength(128);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Current")
                        .HasColumnType("text");

                    b.Property<string>("Original")
                        .HasColumnType("text");

                    b.Property<string>("RecordID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Table")
                        .IsRequired()
                        .HasColumnType("character varying(128)")
                        .HasMaxLength(128);

                    b.Property<string>("User")
                        .HasColumnType("character varying(128)")
                        .HasMaxLength(128);

                    b.Property<string>("UserName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

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

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.ToTable("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
