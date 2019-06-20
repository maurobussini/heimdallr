﻿// <auto-generated />
using ZenProgramming.Heimdallr.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace ZenProgramming.Heimdallr.EntityFramework.Migrations
{
    [DbContext(typeof(HeimdallrDbContext))]
    [Migration("20171123165713_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ZenProgramming.Heimdallr.Entities.Audience", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255);

                    b.Property<string>("AllowedOrigin")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("ClientSecret")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<bool>("HasAdministrativeAccess");

                    b.Property<bool>("IsEnabled");

                    b.Property<bool>("IsNative");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<int>("RefreshTokenLifeTime");

                    b.HasKey("Id");

                    b.ToTable("icHEIMDALLR_Audiences");
                });

            modelBuilder.Entity("ZenProgramming.Heimdallr.Entities.RefreshToken", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255);

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("ExpiresUtc");

                    b.Property<DateTime>("IssuedUtc");

                    b.Property<string>("ProtectedTicket")
                        .IsRequired()
                        .HasMaxLength(2000);

                    b.Property<string>("TokenHash")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("icHEIMDALLR_RefreshTokens");
                });

            modelBuilder.Entity("ZenProgramming.Heimdallr.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<bool>("IsEnabled");

                    b.Property<bool>("IsLocked");

                    b.Property<DateTime?>("LastAccessDate");

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(1024);

                    b.Property<string>("PersonName")
                        .HasMaxLength(255);

                    b.Property<string>("PersonSurname")
                        .HasMaxLength(255);

                    b.Property<byte[]>("PhotoBinary");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("icHEIMDALLR_Users");
                });
#pragma warning restore 612, 618
        }
    }
}
