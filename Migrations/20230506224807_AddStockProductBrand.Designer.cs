﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using web_app_rest.Context;

#nullable disable

namespace web_app_rest.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230506224807_AddStockProductBrand")]
    partial class AddStockProductBrand
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("web_app_rest.Models.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("brands");
                });

            modelBuilder.Entity("web_app_rest.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("products");
                });

            modelBuilder.Entity("web_app_rest.Models.ProductBrand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BrandId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("ProductId");

                    b.ToTable("products-brands");
                });

            modelBuilder.Entity("web_app_rest.Models.SuperMarket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("supermarkets");
                });

            modelBuilder.Entity("web_app_rest.Models.SuperMarketBrand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BrandId")
                        .HasColumnType("int");

                    b.Property<int?>("SuperMarketId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("SuperMarketId");

                    b.ToTable("supermarket-brands");
                });

            modelBuilder.Entity("web_app_rest.Models.ProductBrand", b =>
                {
                    b.HasOne("web_app_rest.Models.Brand", "Brand")
                        .WithMany("ProductBrands")
                        .HasForeignKey("BrandId");

                    b.HasOne("web_app_rest.Models.Product", "Product")
                        .WithMany("ProductBrands")
                        .HasForeignKey("ProductId");

                    b.Navigation("Brand");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("web_app_rest.Models.SuperMarketBrand", b =>
                {
                    b.HasOne("web_app_rest.Models.Brand", "Brand")
                        .WithMany("SuperMarketBrands")
                        .HasForeignKey("BrandId");

                    b.HasOne("web_app_rest.Models.SuperMarket", "SuperMarket")
                        .WithMany("SuperMarketBrands")
                        .HasForeignKey("SuperMarketId");

                    b.Navigation("Brand");

                    b.Navigation("SuperMarket");
                });

            modelBuilder.Entity("web_app_rest.Models.Brand", b =>
                {
                    b.Navigation("ProductBrands");

                    b.Navigation("SuperMarketBrands");
                });

            modelBuilder.Entity("web_app_rest.Models.Product", b =>
                {
                    b.Navigation("ProductBrands");
                });

            modelBuilder.Entity("web_app_rest.Models.SuperMarket", b =>
                {
                    b.Navigation("SuperMarketBrands");
                });
#pragma warning restore 612, 618
        }
    }
}
