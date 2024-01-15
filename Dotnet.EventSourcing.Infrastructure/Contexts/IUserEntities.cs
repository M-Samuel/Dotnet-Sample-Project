using System;
using Dotnet.EventSourcing.Domain.UserDomain;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.EventSourcing.Infrastructure.Contexts
{
    public interface IUserEntities
    {
        DbSet<User> Users { get; set; }
    }
}

