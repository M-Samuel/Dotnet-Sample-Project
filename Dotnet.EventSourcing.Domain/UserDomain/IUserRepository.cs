using System;
namespace Dotnet.EventSourcing.Domain.UserDomain
{
	public interface IUserRepository
	{
		Task<User> GetUserById(Guid userId);
	}
}

