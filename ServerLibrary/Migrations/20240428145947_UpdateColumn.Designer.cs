﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ServerLibrary.Data;

#nullable disable

namespace ServerLibrary.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240428145947_UpdateColumn")]
    partial class UpdateColumn
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BaseLibrary.Entities.ApplicationUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ApplicationUsers");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Branch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DepartmentId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("BaseLibrary.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CountryId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("GeneralDepartmentId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("GeneralDepartmentId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Doctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("integer");

                    b.Property<string>("MedicalDiagnose")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MedicalRecommendation")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("BranchId")
                        .HasColumnType("integer");

                    b.Property<string>("CivilId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FileNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("JobName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Other")
                        .HasColumnType("text");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TelephoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TownId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("TownId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("BaseLibrary.Entities.GeneralDepartment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("GeneralDepartments");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Overtime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("EmployeeId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("OvertimeTypeId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("OvertimeTypeId");

                    b.ToTable("Overtimes");
                });

            modelBuilder.Entity("BaseLibrary.Entities.OvertimeType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("OvertimeTypes");
                });

            modelBuilder.Entity("BaseLibrary.Entities.RefreshTokenInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("RefreshTokenInfos");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Sanction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("integer");

                    b.Property<string>("Punishment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("PunishmentDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("SanctionTypeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SanctionTypeId");

                    b.ToTable("Sanctions");
                });

            modelBuilder.Entity("BaseLibrary.Entities.SanctionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("SanctionTypes");
                });

            modelBuilder.Entity("BaseLibrary.Entities.SystemRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("SystemRoles");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Town", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CityId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Towns");
                });

            modelBuilder.Entity("BaseLibrary.Entities.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Vacation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("EmployeeId")
                        .HasColumnType("integer");

                    b.Property<int>("NumberOfDays")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("VacationTypeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("VacationTypeId");

                    b.ToTable("Vacations");
                });

            modelBuilder.Entity("BaseLibrary.Entities.VacationType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("VacationTypes");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Branch", b =>
                {
                    b.HasOne("BaseLibrary.Entities.Department", "Department")
                        .WithMany("Branches")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("BaseLibrary.Entities.City", b =>
                {
                    b.HasOne("BaseLibrary.Entities.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Department", b =>
                {
                    b.HasOne("BaseLibrary.Entities.GeneralDepartment", "GeneralDepartment")
                        .WithMany("Departments")
                        .HasForeignKey("GeneralDepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GeneralDepartment");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Employee", b =>
                {
                    b.HasOne("BaseLibrary.Entities.Branch", "Branch")
                        .WithMany("Employees")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BaseLibrary.Entities.Town", "Town")
                        .WithMany("Employees")
                        .HasForeignKey("TownId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");

                    b.Navigation("Town");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Overtime", b =>
                {
                    b.HasOne("BaseLibrary.Entities.OvertimeType", "OvertimeType")
                        .WithMany("Overtimes")
                        .HasForeignKey("OvertimeTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OvertimeType");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Sanction", b =>
                {
                    b.HasOne("BaseLibrary.Entities.SanctionType", "SanctionType")
                        .WithMany("Sanctions")
                        .HasForeignKey("SanctionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SanctionType");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Town", b =>
                {
                    b.HasOne("BaseLibrary.Entities.City", "City")
                        .WithMany("Towns")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Vacation", b =>
                {
                    b.HasOne("BaseLibrary.Entities.VacationType", "VacationType")
                        .WithMany("Vacations")
                        .HasForeignKey("VacationTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VacationType");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Branch", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("BaseLibrary.Entities.City", b =>
                {
                    b.Navigation("Towns");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Country", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Department", b =>
                {
                    b.Navigation("Branches");
                });

            modelBuilder.Entity("BaseLibrary.Entities.GeneralDepartment", b =>
                {
                    b.Navigation("Departments");
                });

            modelBuilder.Entity("BaseLibrary.Entities.OvertimeType", b =>
                {
                    b.Navigation("Overtimes");
                });

            modelBuilder.Entity("BaseLibrary.Entities.SanctionType", b =>
                {
                    b.Navigation("Sanctions");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Town", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("BaseLibrary.Entities.VacationType", b =>
                {
                    b.Navigation("Vacations");
                });
#pragma warning restore 612, 618
        }
    }
}
