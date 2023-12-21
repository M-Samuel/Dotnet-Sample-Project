using System;
using Dotnet.EventSourcing.Infrastructure.DTO.UserDTO;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.EventSourcing.Infrastructure.Contexts
{
    public class DatabaseContext : DbContext, IUserEntities
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}

