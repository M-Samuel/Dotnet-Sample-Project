using System;
using Dotnet.EventSourcing.Domain.UserDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dotnet.EventSourcing.Infrastructure.Contexts
{
    public interface IUserEntities
    {
        DbSet<User> Users { get; set; }
        void UserBuilder(EntityTypeBuilder<User> builder);
    }
}

