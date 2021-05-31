﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(AppDataBaseContext))]
    [Migration("20210531011413_InitDB")]
    partial class InitDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Infrastructure.Entity.DocumentFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("File")
                        .IsRequired()
                        .HasColumnType("blob");

                    b.HasKey("Id");

                    b.ToTable("DocumentFiles");
                });

            modelBuilder.Entity("Infrastructure.Entity.RoleUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RoleUsers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Role = "User"
                        },
                        new
                        {
                            Id = 2,
                            Role = "Admin"
                        });
                });

            modelBuilder.Entity("Infrastructure.Entity.TypeDocument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DocumentFileId")
                        .HasColumnType("int");

                    b.Property<int>("KodDocumentFile")
                        .HasColumnType("int");

                    b.Property<int>("KodUser")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DocumentFileId");

                    b.HasIndex("UserId");

                    b.ToTable("TypeDocuments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            KodDocumentFile = 1,
                            KodUser = 2,
                            Type = "Passport"
                        });
                });

            modelBuilder.Entity("Infrastructure.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("KodRole")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("RoleUserId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("Not approved");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.HasIndex("RoleUserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "qwerty@gmail.com",
                            KodRole = 1,
                            Password = "4A7D1ED414474E4033AC29CCB8653D9B",
                            UserName = "Иван"
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "1234@gmail.com",
                            KodRole = 2,
                            Password = "B59C67BF196A4758191E42F76670CEBA",
                            Status = "Approved",
                            UserName = "Николай"
                        });
                });

            modelBuilder.Entity("Infrastructure.Entity.TypeDocument", b =>
                {
                    b.HasOne("Infrastructure.Entity.DocumentFile", null)
                        .WithMany("TypeDocuments")
                        .HasForeignKey("DocumentFileId");

                    b.HasOne("Infrastructure.Entity.User", null)
                        .WithMany("TypeDocuments")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Infrastructure.Entity.User", b =>
                {
                    b.HasOne("Infrastructure.Entity.RoleUser", null)
                        .WithMany("Users")
                        .HasForeignKey("RoleUserId");
                });

            modelBuilder.Entity("Infrastructure.Entity.DocumentFile", b =>
                {
                    b.Navigation("TypeDocuments");
                });

            modelBuilder.Entity("Infrastructure.Entity.RoleUser", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Infrastructure.Entity.User", b =>
                {
                    b.Navigation("TypeDocuments");
                });
#pragma warning restore 612, 618
        }
    }
}
