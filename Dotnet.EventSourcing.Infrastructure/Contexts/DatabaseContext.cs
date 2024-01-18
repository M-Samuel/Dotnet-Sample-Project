using System;
using Dotnet.EventSourcing.Domain.IncidentDomain;
using Dotnet.EventSourcing.Domain.UserDomain;
using Dotnet.EventSourcing.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dotnet.EventSourcing.Infrastructure.Contexts
{
    public class DatabaseContext : DbContext, IUserEntities, IIncidentEntities, IUnitOfWork
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(UserBuilder);
            modelBuilder.Entity<Incident>(IncidentBuilder);
            modelBuilder.Entity<IncidentStatusChange>(IncidentStatusChangeBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Incident> Incidents { get; set; }
		public DbSet<IncidentStatusChange> IncidentStatusChanges { get; set; }


        public void UserBuilder(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.FirstName);
            builder.Property(u => u.LastName);
        }

        public void IncidentBuilder(EntityTypeBuilder<Incident> builder)
        {
            builder.HasKey(u => u.Id);
            builder
            .HasOne(i => i.Assignee)
            .WithMany(u => u.IncidentsAsAssignee)
            .HasForeignKey(i => i.AssigneeId);

            builder
            .HasOne(i => i.Customer)
            .WithMany(u => u.IncidentsAsCustomer)
            .HasForeignKey(i => i.CustomerId);


            builder.Property(i => i.Status);
            builder.Property(i => i.CreatedDate);
            builder.Property(i => i.Description);
            builder.Property(i => i.Title);
        }

        public void IncidentStatusChangeBuilder(EntityTypeBuilder<IncidentStatusChange> builder)
        {
            builder.HasKey(isc => isc.Id);
            builder
            .HasOne(isc => isc.Incident)
            .WithMany(i => i.IncidentStatusChanges)
            .HasForeignKey(isc => isc.IncidentId);

            builder
            .HasOne(isc => isc.ChangedBy)
            .WithMany(u => u.IncidentStatusChanges)
            .HasForeignKey(isc => isc.ChangedByUserId);


            builder.Property(isc => isc.NewStatus);
            builder.Property(isc => isc.OldStatus);
            builder.Property(isc => isc.ChangedDateTime);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsyncWithCancellationToken(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}

