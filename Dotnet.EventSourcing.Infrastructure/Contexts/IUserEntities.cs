using System;
using Dotnet.EventSourcing.Infrastructure.DTO.UserDTO;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.EventSourcing.Infrastructure.Contexts
{
    public interface IUserEntities
    {
        DbSet<User> Users { get; set; }
    }
}

