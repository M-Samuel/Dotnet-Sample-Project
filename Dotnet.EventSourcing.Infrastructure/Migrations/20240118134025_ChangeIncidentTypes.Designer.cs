﻿// <auto-generated />
using System;
using Dotnet.EventSourcing.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Dotnet.EventSourcing.Infrastructure.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240118134025_ChangeIncidentTypes")]
    partial class ChangeIncidentTypes
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Dotnet.EventSourcing.Domain.IncidentDomain.Incident", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AssigneeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Title")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.HasKey("Id");

                    b.HasIndex("AssigneeId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Incidents");
                });

            modelBuilder.Entity("Dotnet.EventSourcing.Domain.IncidentDomain.IncidentStatusChange", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ChangedByUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ChangedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("IncidentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NewStatus")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("OldStatus")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("ChangedByUserId");

                    b.HasIndex("IncidentId");

                    b.ToTable("IncidentStatusChanges");
                });

            modelBuilder.Entity("Dotnet.EventSourcing.Domain.UserDomain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Dotnet.EventSourcing.Domain.IncidentDomain.Incident", b =>
                {
                    b.HasOne("Dotnet.EventSourcing.Domain.UserDomain.User", "Assignee")
                        .WithMany("IncidentsAsAssignee")
                        .HasForeignKey("AssigneeId");

                    b.HasOne("Dotnet.EventSourcing.Domain.UserDomain.User", "Customer")
                        .WithMany("IncidentsAsCustomer")
                        .HasForeignKey("CustomerId");

                    b.Navigation("Assignee");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Dotnet.EventSourcing.Domain.IncidentDomain.IncidentStatusChange", b =>
                {
                    b.HasOne("Dotnet.EventSourcing.Domain.UserDomain.User", "ChangedBy")
                        .WithMany("IncidentStatusChanges")
                        .HasForeignKey("ChangedByUserId");

                    b.HasOne("Dotnet.EventSourcing.Domain.IncidentDomain.Incident", "Incident")
                        .WithMany("IncidentStatusChanges")
                        .HasForeignKey("IncidentId");

                    b.Navigation("ChangedBy");

                    b.Navigation("Incident");
                });

            modelBuilder.Entity("Dotnet.EventSourcing.Domain.IncidentDomain.Incident", b =>
                {
                    b.Navigation("IncidentStatusChanges");
                });

            modelBuilder.Entity("Dotnet.EventSourcing.Domain.UserDomain.User", b =>
                {
                    b.Navigation("IncidentStatusChanges");

                    b.Navigation("IncidentsAsAssignee");

                    b.Navigation("IncidentsAsCustomer");
                });
#pragma warning restore 612, 618
        }
    }
}
