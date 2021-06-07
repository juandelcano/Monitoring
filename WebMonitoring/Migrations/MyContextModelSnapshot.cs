﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebMonitoring.Context;

namespace WebMonitoring.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebMonitoring.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("TB_M_Employee");
                });

            modelBuilder.Entity("WebMonitoring.Models.KinerjaPerbankan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("ATMR")
                        .HasColumnType("float");

                    b.Property<double>("BOPO")
                        .HasColumnType("float");

                    b.Property<double>("BebanOperasional")
                        .HasColumnType("float");

                    b.Property<double>("CAR")
                        .HasColumnType("float");

                    b.Property<double>("DanaPihakKetiga")
                        .HasColumnType("float");

                    b.Property<double>("KreditKol1")
                        .HasColumnType("float");

                    b.Property<double>("KreditKol2")
                        .HasColumnType("float");

                    b.Property<double>("KreditKol3")
                        .HasColumnType("float");

                    b.Property<double>("KreditKol4")
                        .HasColumnType("float");

                    b.Property<double>("KreditKol5")
                        .HasColumnType("float");

                    b.Property<double>("LDR")
                        .HasColumnType("float");

                    b.Property<double>("Laba")
                        .HasColumnType("float");

                    b.Property<double>("Modal")
                        .HasColumnType("float");

                    b.Property<double>("NPL")
                        .HasColumnType("float");

                    b.Property<double>("PendapatanOperasional")
                        .HasColumnType("float");

                    b.Property<DateTime>("Periode")
                        .HasColumnType("datetime2");

                    b.Property<double>("ROA")
                        .HasColumnType("float");

                    b.Property<double>("ROE")
                        .HasColumnType("float");

                    b.Property<string>("SandiBank")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TotalAset")
                        .HasColumnType("float");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TB_M_KinerjaPerbankan");
                });

            modelBuilder.Entity("WebMonitoring.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TB_M_Role");
                });

            modelBuilder.Entity("WebMonitoring.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsUpdatePassword")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TB_M_User");
                });

            modelBuilder.Entity("WebMonitoring.Models.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("TB_T_UserRole");
                });

            modelBuilder.Entity("WebMonitoring.Models.Employee", b =>
                {
                    b.HasOne("WebMonitoring.Models.User", "User")
                        .WithOne("Employee")
                        .HasForeignKey("WebMonitoring.Models.Employee", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebMonitoring.Models.UserRole", b =>
                {
                    b.HasOne("WebMonitoring.Models.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebMonitoring.Models.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebMonitoring.Models.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("WebMonitoring.Models.User", b =>
                {
                    b.Navigation("Employee");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
