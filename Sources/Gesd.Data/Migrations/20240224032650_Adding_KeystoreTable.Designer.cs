﻿// <auto-generated />
using System;
using Gesd.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Gesd.Data.Migrations
{
    [DbContext(typeof(GesdContext))]
    [Migration("20240224032650_Adding_KeystoreTable")]
    partial class Adding_KeystoreTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Gesd.Entite.EncryptedUrlFile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EncryptedUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("FileId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("FileId")
                        .IsUnique();

                    b.ToTable("EncryptedUrlFiles");
                });

            modelBuilder.Entity("Gesd.Entite.File", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("FileSize")
                        .HasColumnType("float");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModify")
                        .HasColumnType("datetime2");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("Gesd.Entite.KeyStore", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EncryptedUrlId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("GeneratedKey")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EncryptedUrlId")
                        .IsUnique();

                    b.ToTable("KeyStores");
                });

            modelBuilder.Entity("Gesd.Entite.EncryptedUrlFile", b =>
                {
                    b.HasOne("Gesd.Entite.File", "File")
                        .WithOne("EncryptedUrlFile")
                        .HasForeignKey("Gesd.Entite.EncryptedUrlFile", "FileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("File");
                });

            modelBuilder.Entity("Gesd.Entite.KeyStore", b =>
                {
                    b.HasOne("Gesd.Entite.EncryptedUrlFile", "EncryptedUrlFile")
                        .WithOne("GenerateStoredKey")
                        .HasForeignKey("Gesd.Entite.KeyStore", "EncryptedUrlId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EncryptedUrlFile");
                });

            modelBuilder.Entity("Gesd.Entite.EncryptedUrlFile", b =>
                {
                    b.Navigation("GenerateStoredKey")
                        .IsRequired();
                });

            modelBuilder.Entity("Gesd.Entite.File", b =>
                {
                    b.Navigation("EncryptedUrlFile")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
