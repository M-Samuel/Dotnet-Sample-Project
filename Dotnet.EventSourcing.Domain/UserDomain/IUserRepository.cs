using System;
using Dotnet.EventSourcing.SharedKernel;

namespace Dotnet.EventSourcing.Domain.UserDomain
{
	public interface IUserRepository
	{
        IUnitOfWork UnitOfWork { get; }
		Task<User?> GetUserByIdAsync(Guid userId);
        Task<User?> GetUserByNameAsync(string firstName, string lastName);
        Task CreateUserAsync(User user);


    }
}

