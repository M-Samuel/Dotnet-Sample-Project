using System;
using Dotnet.EventSourcing.SharedKernel;

namespace Dotnet.EventSourcing.Domain.UserDomain
{
	public class User: Entity
	{
        public User(Guid id, FullName fullName) : base(id)
        {
            FullName = fullName;
        }

        public FullName FullName { get; set; }

        public static User CreateNew(FullName fullName)
        {
            User user = new(Guid.NewGuid(), fullName);
            return user;
        }
    }

    public record FullName(string FirstName, string LastName);
}

