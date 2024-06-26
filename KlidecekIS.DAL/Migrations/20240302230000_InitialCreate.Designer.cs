﻿// <auto-generated />
using System;
using KlidecekIS.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KlidecekIS.DAL.Migrations
{
    [DbContext(typeof(KlidecekDbContext))]
    [Migration("20240302230000_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.1");

            modelBuilder.Entity("KlidecekIS.DAL.Entities.ActivityEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("ActivityType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("End")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("GradeId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Start")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SubjectId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.HasIndex("SubjectId");

                    b.ToTable("ActivityEntities");
                });

            modelBuilder.Entity("KlidecekIS.DAL.Entities.GradeEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ActivityId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Score")
                        .HasColumnType("REAL");

                    b.Property<Guid>("StudentEntityId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId")
                        .IsUnique();

                    b.HasIndex("StudentEntityId");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("KlidecekIS.DAL.Entities.RoomEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("KlidecekIS.DAL.Entities.StudentEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("KlidecekIS.DAL.Entities.SubjectEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Short")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("StudentEntitySubjectEntity", b =>
                {
                    b.Property<Guid>("StudentsId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SubjectsId")
                        .HasColumnType("TEXT");

                    b.HasKey("StudentsId", "SubjectsId");

                    b.HasIndex("SubjectsId");

                    b.ToTable("StudentEntitySubjectEntity");
                });

            modelBuilder.Entity("KlidecekIS.DAL.Entities.ActivityEntity", b =>
                {
                    b.HasOne("KlidecekIS.DAL.Entities.RoomEntity", "Room")
                        .WithMany("Activites")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KlidecekIS.DAL.Entities.SubjectEntity", "Subject")
                        .WithMany("Activites")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("KlidecekIS.DAL.Entities.GradeEntity", b =>
                {
                    b.HasOne("KlidecekIS.DAL.Entities.ActivityEntity", "Activity")
                        .WithOne("Grade")
                        .HasForeignKey("KlidecekIS.DAL.Entities.GradeEntity", "ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KlidecekIS.DAL.Entities.StudentEntity", "StudentEntity")
                        .WithMany()
                        .HasForeignKey("StudentEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Activity");

                    b.Navigation("StudentEntity");
                });

            modelBuilder.Entity("StudentEntitySubjectEntity", b =>
                {
                    b.HasOne("KlidecekIS.DAL.Entities.StudentEntity", null)
                        .WithMany()
                        .HasForeignKey("StudentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KlidecekIS.DAL.Entities.SubjectEntity", null)
                        .WithMany()
                        .HasForeignKey("SubjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("KlidecekIS.DAL.Entities.ActivityEntity", b =>
                {
                    b.Navigation("Grade")
                        .IsRequired();
                });

            modelBuilder.Entity("KlidecekIS.DAL.Entities.RoomEntity", b =>
                {
                    b.Navigation("Activites");
                });

            modelBuilder.Entity("KlidecekIS.DAL.Entities.SubjectEntity", b =>
                {
                    b.Navigation("Activites");
                });
#pragma warning restore 612, 618
        }
    }
}
