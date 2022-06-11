﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using e_ticaret.Models;

#nullable disable

namespace e_ticaret.Migrations
{
    [DbContext(typeof(eTicaretContext))]
    [Migration("20220611200149_yeni2")]
    partial class yeni2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("e_ticaret.Models.Brand", b =>
                {
                    b.Property<short>("BrandId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<short>("BrandId"), 1L, 1);

                    b.Property<string>("BrandName")
                        .IsRequired()
                        .HasColumnType("nchar(50)");

                    b.HasKey("BrandId");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("e_ticaret.Models.Category", b =>
                {
                    b.Property<short>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<short>("CategoryId"), 1L, 1);

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nchar(50)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("e_ticaret.Models.City", b =>
                {
                    b.Property<short>("CityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<short>("CityId"), 1L, 1);

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasColumnType("nchar(20)");

                    b.HasKey("CityId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("e_ticaret.Models.Customer", b =>
                {
                    b.Property<long>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("CustomerId"), 1L, 1);

                    b.Property<string>("CustomerAddress")
                        .IsRequired()
                        .HasColumnType("nchar(200)");

                    b.Property<string>("CustomerEmail")
                        .IsRequired()
                        .HasColumnType("char(100)");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("nchar(50)");

                    b.Property<string>("CustomerPassword")
                        .HasColumnType("char(64)");

                    b.Property<string>("CustomerPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerSurname")
                        .IsRequired()
                        .HasColumnType("nchar(50)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("e_ticaret.Models.ItemStatus", b =>
                {
                    b.Property<short>("ItemStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<short>("ItemStatusId"), 1L, 1);

                    b.Property<string>("ItemStatusName")
                        .IsRequired()
                        .HasColumnType("nchar(50)");

                    b.HasKey("ItemStatusId");

                    b.ToTable("ItemStatuses");
                });

            modelBuilder.Entity("e_ticaret.Models.Order", b =>
                {
                    b.Property<long>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("OrderId"), 1L, 1);

                    b.Property<bool>("AllDelivered")
                        .HasColumnType("bit");

                    b.Property<bool>("Cancelled")
                        .HasColumnType("bit");

                    b.Property<long>("CustomerId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsCart")
                        .HasColumnType("bit");

                    b.Property<float>("OrderPrice")
                        .HasColumnType("real");

                    b.Property<bool>("PaymentComplete")
                        .HasColumnType("bit");

                    b.Property<short?>("PaymentMethodId")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("PaymentMethodId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("e_ticaret.Models.OrderDetail", b =>
                {
                    b.Property<long>("OrderDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("OrderDetailId"), 1L, 1);

                    b.Property<byte>("Count")
                        .HasColumnType("tinyint");

                    b.Property<long>("OrderId")
                        .HasColumnType("bigint");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.HasKey("OrderDetailId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("e_ticaret.Models.OrderDetailStatus", b =>
                {
                    b.Property<long>("OrderDetailStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("OrderDetailStatusId"), 1L, 1);

                    b.Property<DateTime>("ChangeItemStatus")
                        .HasColumnType("datetime2");

                    b.Property<short>("ItemStatusId")
                        .HasColumnType("smallint");

                    b.Property<long>("OrderDetailId")
                        .HasColumnType("bigint");

                    b.HasKey("OrderDetailStatusId");

                    b.HasIndex("ItemStatusId");

                    b.HasIndex("OrderDetailId");

                    b.ToTable("OrderDetailStatuses");
                });

            modelBuilder.Entity("e_ticaret.Models.PaymentMethod", b =>
                {
                    b.Property<short>("PaymentMethodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<short>("PaymentMethodId"), 1L, 1);

                    b.Property<string>("PaymentMethodName")
                        .IsRequired()
                        .HasColumnType("nchar(30)");

                    b.HasKey("PaymentMethodId");

                    b.ToTable("PaymentMethods");
                });

            modelBuilder.Entity("e_ticaret.Models.Product", b =>
                {
                    b.Property<long>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ProductId"), 1L, 1);

                    b.Property<short>("BrandId")
                        .HasColumnType("smallint");

                    b.Property<short>("CategoryId")
                        .HasColumnType("smallint");

                    b.Property<string>("Description")
                        .HasColumnType("nchar(200)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nchar(100)");

                    b.Property<float>("ProductPrice")
                        .HasColumnType("real");

                    b.Property<float?>("ProductRate")
                        .HasColumnType("real");

                    b.Property<int>("SellerId")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("SellerId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("e_ticaret.Models.Seller", b =>
                {
                    b.Property<int>("SellerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SellerId"), 1L, 1);

                    b.Property<bool>("Banned")
                        .HasColumnType("bit");

                    b.Property<short>("CityId")
                        .HasColumnType("smallint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SellerDescription")
                        .HasColumnType("nchar(200)");

                    b.Property<string>("SellerEMail")
                        .IsRequired()
                        .HasColumnType("char(100)");

                    b.Property<string>("SellerName")
                        .IsRequired()
                        .HasColumnType("nchar(50)");

                    b.Property<string>("SellerPassword")
                        .IsRequired()
                        .HasColumnType("char(64)");

                    b.Property<float?>("SellerRate")
                        .HasColumnType("real");

                    b.HasKey("SellerId");

                    b.HasIndex("CityId");

                    b.ToTable("Sellers");
                });

            modelBuilder.Entity("e_ticaret.Models.Order", b =>
                {
                    b.HasOne("e_ticaret.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("e_ticaret.Models.PaymentMethod", "PaymentMethod")
                        .WithMany()
                        .HasForeignKey("PaymentMethodId");

                    b.Navigation("Customer");

                    b.Navigation("PaymentMethod");
                });

            modelBuilder.Entity("e_ticaret.Models.OrderDetail", b =>
                {
                    b.HasOne("e_ticaret.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("e_ticaret.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("e_ticaret.Models.OrderDetailStatus", b =>
                {
                    b.HasOne("e_ticaret.Models.ItemStatus", "ItemStatus")
                        .WithMany()
                        .HasForeignKey("ItemStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("e_ticaret.Models.OrderDetail", "OrderDetail")
                        .WithMany("OrderDetailStatuses")
                        .HasForeignKey("OrderDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ItemStatus");

                    b.Navigation("OrderDetail");
                });

            modelBuilder.Entity("e_ticaret.Models.Product", b =>
                {
                    b.HasOne("e_ticaret.Models.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("e_ticaret.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("e_ticaret.Models.Seller", "Seller")
                        .WithMany("Products")
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Category");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("e_ticaret.Models.Seller", b =>
                {
                    b.HasOne("e_ticaret.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("e_ticaret.Models.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("e_ticaret.Models.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("e_ticaret.Models.OrderDetail", b =>
                {
                    b.Navigation("OrderDetailStatuses");
                });

            modelBuilder.Entity("e_ticaret.Models.Seller", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
