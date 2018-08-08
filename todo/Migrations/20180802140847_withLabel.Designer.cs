﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using todo.Models;

namespace todo.Migrations
{
    [DbContext(typeof(todoContext))]
    [Migration("20180802140847_withLabel")]
    partial class withLabel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("todo.Model.Checklist", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DataID");

                    b.Property<string>("text");

                    b.HasKey("ID");

                    b.HasIndex("DataID");

                    b.ToTable("Checklist");
                });

            modelBuilder.Entity("todo.Model.Data", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsPinned");

                    b.Property<string>("Text");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Data");
                });

            modelBuilder.Entity("todo.Model.Label", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DataID");

                    b.Property<string>("text");

                    b.HasKey("ID");

                    b.HasIndex("DataID");

                    b.ToTable("Label");
                });

            modelBuilder.Entity("todo.Model.Checklist", b =>
                {
                    b.HasOne("todo.Model.Data")
                        .WithMany("checklist")
                        .HasForeignKey("DataID");
                });

            modelBuilder.Entity("todo.Model.Label", b =>
                {
                    b.HasOne("todo.Model.Data")
                        .WithMany("label")
                        .HasForeignKey("DataID");
                });
#pragma warning restore 612, 618
        }
    }
}