using System;
using Dotnet.EventSourcing.SharedKernel;

namespace Dotnet.EventSourcing.Domain.UserDomain
{
	public class User: IEntity
	{
        public required Guid Id { get; set; }

        public required FullName FullName { get; set; }
    }

    public record FullName(string FirstName, string LastName);
}

