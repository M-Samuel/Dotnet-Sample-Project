using System;
using Dotnet.EventSourcing.Domain.UserDomain.UserDomainEvents;
using Dotnet.EventSourcing.SharedKernel;

namespace Dotnet.EventSourcing.Domain.UserDomain
{
	public interface IUserService
	{
		Task<Result<User>> ProcessDomainEvent(CreateUserEvent createUserEvent);
	}
}

