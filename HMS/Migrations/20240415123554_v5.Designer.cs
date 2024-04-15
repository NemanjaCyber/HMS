﻿// <auto-generated />
using System;
using HMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HMS.Migrations
{
    [DbContext(typeof(HMSContext))]
    [Migration("20240415123554_v5")]
    partial class v5
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HMS.Models.MedicalHistory", b =>
                {
                    b.Property<int>("Record_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Record_ID"));

                    b.Property<string>("Alergies")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Patient_ID")
                        .HasColumnType("int");

                    b.Property<string>("Pre_Conditions")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Record_ID");

                    b.HasIndex("Patient_ID");

                    b.ToTable("MedicalHistories");
                });

            modelBuilder.Entity("HMS.Models.Medicine", b =>
                {
                    b.Property<int>("Medicine_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Medicine_ID"));

                    b.Property<decimal>("M_Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("M_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("M_Quantity")
                        .HasColumnType("int");

                    b.HasKey("Medicine_ID");

                    b.ToTable("Medicines");
                });

            modelBuilder.Entity("HMS.Models.Patient", b =>
                {
                    b.Property<int>("Patient_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Patient_ID"));

                    b.Property<DateTime>("Admision_Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Assigned_Room_IdRoom_ID")
                        .HasColumnType("int");

                    b.Property<string>("Blood_type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Condition")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Discharge_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JMBG")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patient_Fname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patient_Lname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Patient_ID");

                    b.HasIndex("Assigned_Room_IdRoom_ID");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("HMS.Models.Room", b =>
                {
                    b.Property<int>("Room_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Room_ID"));

                    b.Property<int>("Room_Available_Beds")
                        .HasColumnType("int");

                    b.Property<decimal>("Room_Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Room_Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Room_ID");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("HMS.Models.MedicalHistory", b =>
                {
                    b.HasOne("HMS.Models.Patient", "Patient_Id")
                        .WithMany()
                        .HasForeignKey("Patient_ID");

                    b.Navigation("Patient_Id");
                });

            modelBuilder.Entity("HMS.Models.Patient", b =>
                {
                    b.HasOne("HMS.Models.Room", "Assigned_Room_Id")
                        .WithMany("Room_Patients")
                        .HasForeignKey("Assigned_Room_IdRoom_ID");

                    b.Navigation("Assigned_Room_Id");
                });

            modelBuilder.Entity("HMS.Models.Room", b =>
                {
                    b.Navigation("Room_Patients");
                });
#pragma warning restore 612, 618
        }
    }
}
