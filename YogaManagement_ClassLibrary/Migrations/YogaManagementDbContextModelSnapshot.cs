﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YogaManagement_ClassLibrary;

#nullable disable

namespace YogaManagement_ClassLibrary.Migrations
{
    [DbContext(typeof(YogaManagementDbContext))]
    partial class YogaManagementDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("YogaManagement_ClassLibrary.Class", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.PrimitiveCollection<string>("AvailableFormats")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ClassEnd")
                        .HasColumnType("TEXT");

                    b.Property<int>("ClassLocationId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ClassStart")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.PrimitiveCollection<string>("Specialities")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ClassLocationId");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("YogaManagement_ClassLibrary.ClassLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FormatsAvailable")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("MaxCapacity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ClassLocations");
                });

            modelBuilder.Entity("YogaManagement_ClassLibrary.Instructor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Specialities")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Instructors");
                });

            modelBuilder.Entity("YogaManagement_ClassLibrary.InstructorClass", b =>
                {
                    b.Property<int>("InstructorId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClassId")
                        .HasColumnType("INTEGER");

                    b.HasKey("InstructorId", "ClassId");

                    b.HasIndex("ClassId");

                    b.ToTable("InstructorClasses");
                });

            modelBuilder.Entity("YogaManagement_ClassLibrary.Class", b =>
                {
                    b.HasOne("YogaManagement_ClassLibrary.ClassLocation", "ClassLocation")
                        .WithMany("Classes")
                        .HasForeignKey("ClassLocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClassLocation");
                });

            modelBuilder.Entity("YogaManagement_ClassLibrary.InstructorClass", b =>
                {
                    b.HasOne("YogaManagement_ClassLibrary.Class", "FK_Class")
                        .WithMany("InstructorClasses")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YogaManagement_ClassLibrary.Instructor", "FK_Instructor")
                        .WithMany("InstructorClasses")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FK_Class");

                    b.Navigation("FK_Instructor");
                });

            modelBuilder.Entity("YogaManagement_ClassLibrary.Class", b =>
                {
                    b.Navigation("InstructorClasses");
                });

            modelBuilder.Entity("YogaManagement_ClassLibrary.ClassLocation", b =>
                {
                    b.Navigation("Classes");
                });

            modelBuilder.Entity("YogaManagement_ClassLibrary.Instructor", b =>
                {
                    b.Navigation("InstructorClasses");
                });
#pragma warning restore 612, 618
        }
    }
}