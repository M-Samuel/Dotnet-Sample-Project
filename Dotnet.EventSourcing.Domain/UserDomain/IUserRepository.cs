using System;
namespace Dotnet.EventSourcing.Domain.UserDomain
{
	public interface IUserRepository
	{
		Task<User?> GetUserById(Guid userId);
        Task<User?> GetUserByName(string firstName, string lastName);
        Task CreateUser(User user);

    }
}

