﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using _1500MentoriaSistema.Data;

namespace _1500MentoriaSistema.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230405204943_Identity-migration-v2")]
    partial class Identitymigrationv2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ActualState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CircleId")
                        .HasColumnType("int");

                    b.Property<bool>("Delivered")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("FinalValue")
                        .HasColumnType("real");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<int?>("PersonId1")
                        .HasColumnType("int");

                    b.Property<float>("Productivity")
                        .HasColumnType("real");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<float>("RealTime")
                        .HasColumnType("real");

                    b.Property<DateTime>("Sprint")
                        .HasColumnType("datetime2");

                    b.Property<float>("TimePlanned")
                        .HasColumnType("real");

                    b.Property<int>("TypeConsultorId")
                        .HasColumnType("int");

                    b.Property<int>("TypeObject")
                        .HasColumnType("int");

                    b.Property<float>("Value")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("CircleId");

                    b.HasIndex("PersonId");

                    b.HasIndex("PersonId1");

                    b.HasIndex("ProjectId");

                    b.HasIndex("TypeConsultorId");

                    b.ToTable("ActualStates");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("BornDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CPF")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CircleId")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Enterprise")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("GraduateDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsStudying")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NivelStudy")
                        .HasColumnType("int");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("Recommendation")
                        .HasColumnType("bit");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("University")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("CircleId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Duration")
                        .HasColumnType("real");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Enterprise")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Finished")
                        .HasColumnType("bit");

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<float>("Value")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("_1500MentoriaSistema.Models.Circle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Circle");
                });

            modelBuilder.Entity("_1500MentoriaSistema.Models.Feedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CircleId")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<float>("Note")
                        .HasColumnType("real");

                    b.Property<string>("OportunityLearning")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ThemeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CircleId");

                    b.HasIndex("ThemeId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("_1500MentoriaSistema.Models.HoursDay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ActualstateId")
                        .HasColumnType("int");

                    b.Property<bool>("Delivered")
                        .HasColumnType("bit");

                    b.Property<float>("Hours")
                        .HasColumnType("real");

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ActualstateId");

                    b.ToTable("HoursDay");
                });

            modelBuilder.Entity("_1500MentoriaSistema.Models.Learning", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CircleId")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LearningAction")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("MeasurementDate")
                        .HasColumnType("datetime2");

                    b.Property<float>("MeasurementForm")
                        .HasColumnType("real");

                    b.Property<string>("OportunityLearning")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Result")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("ThemeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CircleId");

                    b.HasIndex("ThemeId");

                    b.ToTable("Learnings");
                });

            modelBuilder.Entity("_1500MentoriaSistema.Models.PersonFeedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FeedbackId")
                        .HasColumnType("int");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<int?>("PersonId1")
                        .HasColumnType("int");

                    b.Property<int>("TypePerson")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FeedbackId");

                    b.HasIndex("PersonId");

                    b.HasIndex("PersonId1");

                    b.ToTable("PeopleFeedback");
                });

            modelBuilder.Entity("_1500MentoriaSistema.Models.PersonLearning", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LearningId")
                        .HasColumnType("int");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<int?>("PersonId1")
                        .HasColumnType("int");

                    b.Property<int>("TypePerson")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LearningId");

                    b.HasIndex("PersonId");

                    b.HasIndex("PersonId1");

                    b.ToTable("PeopleLearning");
                });

            modelBuilder.Entity("_1500MentoriaSistema.Models.Theme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("Themes");
                });

            modelBuilder.Entity("_1500MentoriaSistema.Models.TypeConsultor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Fee")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TypeConsultor");
                });

            modelBuilder.Entity("ActualState", b =>
                {
                    b.HasOne("_1500MentoriaSistema.Models.Circle", "Circle")
                        .WithMany()
                        .HasForeignKey("CircleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Person", null)
                        .WithMany("PlanResume")
                        .HasForeignKey("PersonId1");

                    b.HasOne("Project", "Project")
                        .WithMany("PlanResumes")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_1500MentoriaSistema.Models.TypeConsultor", "TypeConsultor")
                        .WithMany()
                        .HasForeignKey("TypeConsultorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("Person", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("Person", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Person", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("Person", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Person", b =>
                {
                    b.HasOne("_1500MentoriaSistema.Models.Circle", "Circle")
                        .WithMany("Persons")
                        .HasForeignKey("CircleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("_1500MentoriaSistema.Models.Feedback", b =>
                {
                    b.HasOne("_1500MentoriaSistema.Models.Circle", "Circle")
                        .WithMany()
                        .HasForeignKey("CircleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_1500MentoriaSistema.Models.Theme", "Theme")
                        .WithMany()
                        .HasForeignKey("ThemeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("_1500MentoriaSistema.Models.HoursDay", b =>
                {
                    b.HasOne("ActualState", "ActualState")
                        .WithMany("HoursDay")
                        .HasForeignKey("ActualstateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("_1500MentoriaSistema.Models.Learning", b =>
                {
                    b.HasOne("_1500MentoriaSistema.Models.Circle", "Circle")
                        .WithMany("Learnings")
                        .HasForeignKey("CircleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_1500MentoriaSistema.Models.Theme", "Theme")
                        .WithMany("Learning")
                        .HasForeignKey("ThemeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("_1500MentoriaSistema.Models.PersonFeedback", b =>
                {
                    b.HasOne("_1500MentoriaSistema.Models.Feedback", "Feedback")
                        .WithMany("PersonFeedback")
                        .HasForeignKey("FeedbackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Person", null)
                        .WithMany("PersonFeedbacks")
                        .HasForeignKey("PersonId1");
                });

            modelBuilder.Entity("_1500MentoriaSistema.Models.PersonLearning", b =>
                {
                    b.HasOne("_1500MentoriaSistema.Models.Learning", "Learning")
                        .WithMany("PersonLearning")
                        .HasForeignKey("LearningId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Person", null)
                        .WithMany("PersonLearning")
                        .HasForeignKey("PersonId1");
                });
#pragma warning restore 612, 618
        }
    }
}
