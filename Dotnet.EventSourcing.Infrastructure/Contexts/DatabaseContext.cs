using System;
using Dotnet.EventSourcing.Domain.IncidentDomain;
using Dotnet.EventSourcing.Domain.UserDomain;
using Dotnet.EventSourcing.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dotnet.EventSourcing.Infrastructure.Contexts
{
    public class DatabaseContext : DbContext, IUnitOfWork, IUserEntities
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<User>(
            //     u => {
            //         u.HasKey(o => o.Id);
            //         u.Property(o => o.FirstName);
            //         u.Property(o => o.LastName);
            //     }
            // );

            // base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        // public DbSet<Incident> Incidents { get; set; }
		// public DbSet<IncidentStatusChange> IncidentStatusChanges { get; set; }

        public async Task CommitChangesAsync()
        {
            await SaveChangesAsync();
        }

        public void UserBuilder(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.FirstName);
            builder.Property(u => u.LastName);
        }
    }
}

