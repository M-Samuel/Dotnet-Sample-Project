using System;
using Dotnet.EventSourcing.SharedKernel;

namespace Dotnet.EventSourcing.Domain.UserDomain
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);
        Task<User?> GetUserByNameAsync(string firstName, string lastName, CancellationToken cancellationToken);
        Task CreateUserAsync(User user, CancellationToken cancellationToken);


    }
}

