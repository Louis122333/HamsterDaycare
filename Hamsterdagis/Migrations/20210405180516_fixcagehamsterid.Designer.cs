﻿// <auto-generated />
using System;
using Hamsterdagis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Hamsterdagis.Migrations
{
    [DbContext(typeof(HamsterDBContext))]
    [Migration("20210405180516_fixcagehamsterid")]
    partial class fixcagehamsterid
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Hamsterdagis.ActivityLog", b =>
                {
                    b.Property<int>("ActivityLogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Activity")
                        .HasColumnType("int");

                    b.Property<int?>("HamsterId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("ActivityLogId");

                    b.HasIndex("HamsterId");

                    b.ToTable("ActivityLogs");
                });

            modelBuilder.Entity("Hamsterdagis.Cage", b =>
                {
                    b.Property<int>("CageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("CageId");

                    b.ToTable("Cages");
                });

            modelBuilder.Entity("Hamsterdagis.ExerciseArea", b =>
                {
                    b.Property<int>("ExerciseAreaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("ExerciseAreaId");

                    b.ToTable("ExerciseArea");
                });

            modelBuilder.Entity("Hamsterdagis.Hamster", b =>
                {
                    b.Property<int>("HamsterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ArrivalTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CageId")
                        .HasColumnType("int");

                    b.Property<int?>("ExerciseAreaId")
                        .HasColumnType("int");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<DateTime?>("LastTimeExercised")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HamsterId");

                    b.HasIndex("CageId");

                    b.HasIndex("ExerciseAreaId");

                    b.ToTable("Hamsters");
                });

            modelBuilder.Entity("Hamsterdagis.ActivityLog", b =>
                {
                    b.HasOne("Hamsterdagis.Hamster", "Hamster")
                        .WithMany("ActivityLogs")
                        .HasForeignKey("HamsterId");

                    b.Navigation("Hamster");
                });

            modelBuilder.Entity("Hamsterdagis.Hamster", b =>
                {
                    b.HasOne("Hamsterdagis.Cage", "Cage")
                        .WithMany("Hamsters")
                        .HasForeignKey("CageId");

                    b.HasOne("Hamsterdagis.ExerciseArea", null)
                        .WithMany("Hamsters")
                        .HasForeignKey("ExerciseAreaId");

                    b.Navigation("Cage");
                });

            modelBuilder.Entity("Hamsterdagis.Cage", b =>
                {
                    b.Navigation("Hamsters");
                });

            modelBuilder.Entity("Hamsterdagis.ExerciseArea", b =>
                {
                    b.Navigation("Hamsters");
                });

            modelBuilder.Entity("Hamsterdagis.Hamster", b =>
                {
                    b.Navigation("ActivityLogs");
                });
#pragma warning restore 612, 618
        }
    }
}
