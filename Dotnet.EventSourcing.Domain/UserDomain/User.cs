using System;
using Dotnet.EventSourcing.SharedKernel;

namespace Dotnet.EventSourcing.Domain.UserDomain
{
	public class User: IEntity
	{
        public Guid Id { get; set; }

        public FullName? FullName { get; set; }
    }

    public record FullName(string FirstName, string LastName);
}

