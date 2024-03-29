﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Server.Models.Contexts;

namespace Server.Models.Contexts.Migrations._1_Init
{
    [DbContext(typeof(WLMDbContext))]
    partial class WLMDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Server.Models.BusinessDay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndDate");

                    b.Property<int>("MonthId");

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("TeamId");

                    b.Property<int>("YearId");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("BusinessDays");
                });

            modelBuilder.Entity("Server.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<int>("FaxNo");

                    b.Property<string>("FirstName");

                    b.Property<int>("HomePhoneNo");

                    b.Property<string>("LastName");

                    b.Property<int>("MobilePhoneNo");

                    b.Property<int>("OfficePhoneNo");

                    b.Property<int>("Role");

                    b.Property<int?>("SeniorId");

                    b.Property<int>("TeamId");

                    b.Property<string>("UserId");

                    b.Property<string>("WebPageAddress");

                    b.HasKey("Id");

                    b.HasIndex("SeniorId");

                    b.HasIndex("TeamId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Server.Models.Hollyday", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedById");

                    b.Property<DateTime>("Date");

                    b.Property<int>("DayOfWeek");

                    b.Property<string>("Description");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("HollyDays");
                });

            modelBuilder.Entity("Server.Models.HollydayTeam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("HollydayId");

                    b.Property<int>("TeamId");

                    b.HasKey("Id");

                    b.HasIndex("HollydayId");

                    b.HasIndex("TeamId");

                    b.ToTable("HollydayTeam");
                });

            modelBuilder.Entity("Server.Models.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BusinessDT");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Description");

                    b.Property<bool>("IsCommissioned");

                    b.Property<int>("PrimaryOwnerId");

                    b.Property<int>("Priority");

                    b.Property<int>("ReviewerId");

                    b.Property<int>("SecondaryOwnerId");

                    b.Property<int>("TeamId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("PrimaryOwnerId");

                    b.HasIndex("ReviewerId");

                    b.HasIndex("SecondaryOwnerId");

                    b.HasIndex("TeamId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("Server.Models.TaskSnapShot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AssignedById");

                    b.Property<int>("AssignedToId");

                    b.Property<int>("PercentageOfWorkCompleted");

                    b.Property<int>("Status");

                    b.Property<int>("TaskId");

                    b.Property<DateTime>("TimeStamp");

                    b.HasKey("Id");

                    b.HasIndex("AssignedById");

                    b.HasIndex("AssignedToId");

                    b.HasIndex("TaskId");

                    b.ToTable("TaskSnapShots");
                });

            modelBuilder.Entity("Server.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Server.Models.BusinessDay", b =>
                {
                    b.HasOne("Server.Models.Team", "Team")
                        .WithMany("BusinessDays")
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("Server.Models.Employee", b =>
                {
                    b.HasOne("Server.Models.Employee", "Senior")
                        .WithMany("Juniors")
                        .HasForeignKey("SeniorId");

                    b.HasOne("Server.Models.Team", "Team")
                        .WithMany("Employees")
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("Server.Models.Hollyday", b =>
                {
                    b.HasOne("Server.Models.Employee", "CreatedBy")
                        .WithMany("CreatedByMeHollydays")
                        .HasForeignKey("CreatedById");
                });

            modelBuilder.Entity("Server.Models.HollydayTeam", b =>
                {
                    b.HasOne("Server.Models.Hollyday", "Hollyday")
                        .WithMany("HollydayTeams")
                        .HasForeignKey("HollydayId");

                    b.HasOne("Server.Models.Team", "Team")
                        .WithMany("HollydayTeams")
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("Server.Models.Task", b =>
                {
                    b.HasOne("Server.Models.Employee", "PrimaryOwner")
                        .WithMany("TasksOfWhichIAmPrimaryOwner")
                        .HasForeignKey("PrimaryOwnerId");

                    b.HasOne("Server.Models.Employee", "Reviewer")
                        .WithMany("TasksOfWhichIAmReviewer")
                        .HasForeignKey("ReviewerId");

                    b.HasOne("Server.Models.Employee", "SecondaryOwner")
                        .WithMany("TasksOfWhichIAmSecondaryOwner")
                        .HasForeignKey("SecondaryOwnerId");

                    b.HasOne("Server.Models.Team", "Team")
                        .WithMany("Tasks")
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("Server.Models.TaskSnapShot", b =>
                {
                    b.HasOne("Server.Models.Employee", "AssignedBy")
                        .WithMany("SnapshotsAssignedByMe")
                        .HasForeignKey("AssignedById");

                    b.HasOne("Server.Models.Employee", "AssignedTo")
                        .WithMany("SnapshotsAssignedToMe")
                        .HasForeignKey("AssignedToId");

                    b.HasOne("Server.Models.Task", "Task")
                        .WithMany("TaskSnapshots")
                        .HasForeignKey("TaskId");
                });
#pragma warning restore 612, 618
        }
    }
}
