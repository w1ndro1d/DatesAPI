﻿// <auto-generated />
using System;
using DatesAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DatesAPI.Migrations
{
    [DbContext(typeof(DateDetailsContext))]
    [Migration("20240410124914_First Migration")]
    partial class FirstMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DatesAPI.Models.DateDetails", b =>
                {
                    b.Property<int>("DateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DateId"));

                    b.Property<string>("Event")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("EventDate")
                        .HasColumnType("datetime");

                    b.Property<string>("EventNote")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("DateId");

                    b.ToTable("DateDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
