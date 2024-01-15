using System;
using Dotnet.EventSourcing.Domain.IncidentDomain;
using Dotnet.EventSourcing.Domain.UserDomain;
using Dotnet.EventSourcing.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.EventSourcing.Infrastructure.Contexts
{
    public class DatabaseContext : DbContext, IUnitOfWork, IUserEntities, IIncidentEntities
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(
                u => {
                    u.HasKey(o => o.Id);
                    u.Property(o => o.FirstName);
                    u.Property(o => o.LastName);
                }
            );

            base.OnModelCreating(modelBuilder);


        }

        public DbSet<User> Users { get; set; }
        public DbSet<Incident> Incidents { get; set; }
		public DbSet<IncidentStatusChange> IncidentStatusChanges { get; set; }

        public async Task CommitChangesAsync()
        {
            await SaveChangesAsync();
        }
    }
}

