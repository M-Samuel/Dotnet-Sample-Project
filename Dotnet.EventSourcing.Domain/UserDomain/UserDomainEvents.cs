using System;
using Dotnet.EventSourcing.SharedKernel;

namespace Dotnet.EventSourcing.Domain.UserDomain.UserDomainEvents
{
	public record CreateUserEvent(DateTime OccurranceDateTime, string FirstName, string LastName) : IDomainEvent;
}

