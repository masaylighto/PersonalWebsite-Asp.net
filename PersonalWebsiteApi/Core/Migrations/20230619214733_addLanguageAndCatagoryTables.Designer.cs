﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PersonalWebsiteApi.Core.Database;

#nullable disable

namespace PersonalWebsiteApi.Core.Migrations
{
    [DbContext(typeof(PostgresDBContext))]
    [Migration("20230619214733_addLanguageAndCatagoryTables")]
    partial class addLanguageAndCatagoryTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PersonalWebsiteApi.Core.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("LanguageId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("PersonalWebsiteApi.Core.Entities.Language", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LanguageName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("WritingDirection")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Languages");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f908fa84-93c9-405d-a160-64a2ec69ac04"),
                            CreateDate = new DateTime(2023, 6, 19, 21, 47, 33, 667, DateTimeKind.Utc).AddTicks(1410),
                            LanguageName = "arabic",
                            UpdateDate = new DateTime(2023, 6, 19, 21, 47, 33, 667, DateTimeKind.Utc).AddTicks(1409),
                            WritingDirection = 0
                        });
                });

            modelBuilder.Entity("PersonalWebsiteApi.Core.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserType")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PersonalWebsiteApi.Core.Entities.Category", b =>
                {
                    b.HasOne("PersonalWebsiteApi.Core.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");
                });
#pragma warning restore 612, 618
        }
    }
}
