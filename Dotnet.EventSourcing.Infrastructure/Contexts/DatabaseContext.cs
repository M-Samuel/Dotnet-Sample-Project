using System;
using Dotnet.EventSourcing.Infrastructure.DTO.IncidentDTO;
using Dotnet.EventSourcing.Infrastructure.DTO.UserDTO;
using Dotnet.EventSourcing.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.EventSourcing.Infrastructure.Contexts
{
    public class DatabaseContext : DbContext, IUnitOfWork, IUserEntities, IIncidentEntities
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
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

