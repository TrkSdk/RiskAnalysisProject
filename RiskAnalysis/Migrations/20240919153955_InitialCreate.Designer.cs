﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RiskAnalysis.Models;

#nullable disable

namespace RiskAnalysis.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240919153955_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RiskAnalysis.Models.Businesses", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BusinessDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BusinessName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RiskFactor")
                        .HasColumnType("int");

                    b.Property<int>("SectorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SectorId")
                        .IsUnique();

                    b.ToTable("Businesses");
                });

            modelBuilder.Entity("RiskAnalysis.Models.Cities", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("RiskAnalysis.Models.Contracts", b =>
                {
                    b.Property<int>("ContractId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ContractId"));

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("ContractName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ContractId");

                    b.HasIndex("Id");

                    b.HasIndex("Id");

                    b.HasIndex("Id");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("RiskAnalysis.Models.Partners", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("ContactEMail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactPerson")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PartnerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RiskFactor")
                        .HasColumnType("int");

                    b.Property<int>("SectorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("SectorId");

                    b.ToTable("Partners");
                });

            modelBuilder.Entity("RiskAnalysis.Models.Risks", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("RiskEstimationSuccess")
                        .HasColumnType("int");

                    b.Property<int>("RiskScore")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Risks");
                });

            modelBuilder.Entity("RiskAnalysis.Models.Sectors", b =>
                {
                    b.Property<int>("SectorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SectorId"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SectorDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SectorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SectorId");

                    b.ToTable("Sectors");
                });

            modelBuilder.Entity("RiskAnalysis.Models.Businesses", b =>
                {
                    b.HasOne("RiskAnalysis.Models.Sectors", "Sector")
                        .WithOne("Business")
                        .HasForeignKey("RiskAnalysis.Models.Businesses", "SectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sector");
                });

            modelBuilder.Entity("RiskAnalysis.Models.Contracts", b =>
                {
                    b.HasOne("RiskAnalysis.Models.Businesses", "Business")
                        .WithMany("ContractsList")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RiskAnalysis.Models.Partners", "Partner")
                        .WithMany("ContractsList")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("RiskAnalysis.Models.Risks", "Risk")
                        .WithMany("ContractsList")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");

                    b.Navigation("Partner");

                    b.Navigation("Risk");
                });

            modelBuilder.Entity("RiskAnalysis.Models.Partners", b =>
                {
                    b.HasOne("RiskAnalysis.Models.Cities", "City")
                        .WithMany("PartnersList")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RiskAnalysis.Models.Sectors", "Sector")
                        .WithMany()
                        .HasForeignKey("SectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("Sector");
                });

            modelBuilder.Entity("RiskAnalysis.Models.Businesses", b =>
                {
                    b.Navigation("ContractsList");
                });

            modelBuilder.Entity("RiskAnalysis.Models.Cities", b =>
                {
                    b.Navigation("PartnersList");
                });

            modelBuilder.Entity("RiskAnalysis.Models.Partners", b =>
                {
                    b.Navigation("ContractsList");
                });

            modelBuilder.Entity("RiskAnalysis.Models.Risks", b =>
                {
                    b.Navigation("ContractsList");
                });

            modelBuilder.Entity("RiskAnalysis.Models.Sectors", b =>
                {
                    b.Navigation("Business")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
