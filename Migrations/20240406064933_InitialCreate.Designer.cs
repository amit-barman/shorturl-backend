﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using shorturl.Model;

#nullable disable

namespace shorturl.Migrations
{
    [DbContext(typeof(ShortUrlDBContext))]
    [Migration("20240406064933_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.3");

            modelBuilder.Entity("shorturl.Model.URL", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("creationTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("realUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("shortUrl")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Urls");
                });
#pragma warning restore 612, 618
        }
    }
}