﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Doctor_Module.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250627051142_AddIsCompletedToAppointmentLogs")]
    partial class AddIsCompletedToAppointmentLogs
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.6");

            modelBuilder.Entity("Appointments.Model.Appointment", b =>
                {
                    b.Property<Guid>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("DoctorId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsCancelled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PatientId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("PaymentStatus")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Remarks")
                        .HasMaxLength(120)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TimeSlot")
                        .HasColumnType("TEXT");

                    b.HasKey("AppointmentId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("Appointments.Model.AppointmentLog", b =>
                {
                    b.Property<int>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("AppointmentId")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("ApprovedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("DoctorID")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PatientId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TimeSlot")
                        .HasColumnType("TEXT");

                    b.HasKey("LogId");

                    b.ToTable("AppointmentLogs");
                });

            modelBuilder.Entity("Doctor_Module.Models.Doctor.Doctor", b =>
                {
                    b.Property<string>("DoctorID")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Experiance")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Specialization")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("DoctorID");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("Doctor_Module.Models.Prescription.Prescription", b =>
                {
                    b.Property<string>("PrescriptionID")
                        .HasColumnType("TEXT");

                    b.Property<string>("DoctorID")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Dosage")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Instructions")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PatientID")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PatientName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("PrescriptionID");

                    b.ToTable("Prescriptions");
                });

            modelBuilder.Entity("Doctor_Module.Timeslots.Timeslot", b =>
                {
                    b.Property<int>("TimeSlotID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Count")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("DoctorID")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("INTEGER");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("TEXT");

                    b.HasKey("TimeSlotID");

                    b.HasIndex("DoctorID");

                    b.ToTable("Timeslots");
                });

            modelBuilder.Entity("Doctor_Module.Timeslots.Timeslot", b =>
                {
                    b.HasOne("Doctor_Module.Models.Doctor.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");
                });
#pragma warning restore 612, 618
        }
    }
}
