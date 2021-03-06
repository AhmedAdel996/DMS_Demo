// <auto-generated />
using System;
using DMS_Demo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DMS_Demo.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210826160215_CreateDb")]
    partial class CreateDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DMS_Demo.Models.Order", b =>
                {
                    b.Property<int>("Order_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Customer_ID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Due_Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Order_Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Order_Status")
                        .HasColumnType("int");

                    b.Property<decimal>("Order_Total")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Request_Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Shipping_ID")
                        .HasColumnType("int");

                    b.Property<decimal>("Tax")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Order_ID");

                    b.HasIndex("Shipping_ID");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("DMS_Demo.Models.OrderDetails", b =>
                {
                    b.Property<int>("OrderDetails_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Order_ID")
                        .HasColumnType("int");

                    b.Property<int>("Product_Color")
                        .HasColumnType("int");

                    b.Property<int>("Product_ID")
                        .HasColumnType("int");

                    b.Property<int>("Product_Quantity")
                        .HasColumnType("int");

                    b.Property<int?>("Product_Size")
                        .HasColumnType("int");

                    b.Property<decimal>("Total_price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UOM_Id")
                        .HasColumnType("int");

                    b.HasKey("OrderDetails_ID");

                    b.HasIndex("Order_ID");

                    b.HasIndex("Product_ID");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("DMS_Demo.Models.Product", b =>
                {
                    b.Property<int>("Product_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Adding_Date")
                        .HasColumnType("date");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Images")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Product_Color")
                        .HasColumnType("int");

                    b.Property<string>("Product_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Product_Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Product_Size")
                        .HasColumnType("int");

                    b.Property<int>("Stored_Quantity")
                        .HasColumnType("int");

                    b.Property<int>("Uom_Id")
                        .HasColumnType("int");

                    b.HasKey("Product_ID");

                    b.HasIndex("Uom_Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("DMS_Demo.Models.Shipping", b =>
                {
                    b.Property<int>("Shipping_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Phone")
                        .HasColumnType("bigint");

                    b.Property<int?>("Postal_Code")
                        .HasColumnType("int");

                    b.Property<string>("Shipper_Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Shipper_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Shipping_ID");

                    b.ToTable("Shipping");
                });

            modelBuilder.Entity("DMS_Demo.Models.UOM", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Uoms");
                });

            modelBuilder.Entity("DMS_Demo.Models.Order", b =>
                {
                    b.HasOne("DMS_Demo.Models.Shipping", "Shipping")
                        .WithMany("Orders")
                        .HasForeignKey("Shipping_ID");

                    b.Navigation("Shipping");
                });

            modelBuilder.Entity("DMS_Demo.Models.OrderDetails", b =>
                {
                    b.HasOne("DMS_Demo.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("Order_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DMS_Demo.Models.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("Product_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("DMS_Demo.Models.Product", b =>
                {
                    b.HasOne("DMS_Demo.Models.UOM", "Uom")
                        .WithMany()
                        .HasForeignKey("Uom_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Uom");
                });

            modelBuilder.Entity("DMS_Demo.Models.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("DMS_Demo.Models.Product", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("DMS_Demo.Models.Shipping", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
