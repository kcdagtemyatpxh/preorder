﻿// <auto-generated />
using System;
using Ef7FirstLook.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Ef7FirstLook.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230713025107_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Ef7FirstLook.Entities.sr_header", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasComment("địa chỉ");

                    b.Property<DateTime>("deliveryDate")
                        .HasColumnType("Date")
                        .HasComment("ngày giao hàng");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasComment("Họ tên khách hàng");

                    b.HasKey("id");

                    b.ToTable("sr_header");
                });
#pragma warning restore 612, 618
        }
    }
}
