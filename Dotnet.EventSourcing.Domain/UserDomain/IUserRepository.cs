using System;
using Dotnet.EventSourcing.SharedKernel;

namespace Dotnet.EventSourcing.Domain.UserDomain
{
	public interface IUserRepository
	{
		Task<User?> GetUserByIdAsync(Guid userId);
        Task<User?> GetUserByNameAsync(string firstName, string lastName);
        Task CreateUserAsync(User user);


    }
}

