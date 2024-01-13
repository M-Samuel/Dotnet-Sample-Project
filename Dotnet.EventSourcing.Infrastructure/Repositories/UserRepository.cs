using System;
using System.Linq;
using Dotnet.EventSourcing.Domain.UserDomain;
using Dotnet.EventSourcing.Infrastructure.Contexts;
using UserDTO = Dotnet.EventSourcing.Infrastructure.DTO.UserDTO;
using UserDomain = Dotnet.EventSourcing.Domain.UserDomain;
using Dotnet.EventSourcing.Infrastructure.DTO.UserDTO;
using Dotnet.EventSourcing.SharedKernel;

namespace Dotnet.EventSourcing.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _databaseContext;

        public UserRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task CreateUserAsync(UserDomain.User user)
        {
            UserDTO.User userDTO = user.ToDTO();
            await _databaseContext.AddAsync(userDTO);
        }

        public async Task<UserDomain.User?> GetUserByIdAsync(Guid userId)
        {
            var users = await GetUsers((query) => query.Where(user => user.Id == userId));
            return await Task.FromResult(users.FirstOrDefault());
        }

        private async Task<IEnumerable<UserDomain.User>> GetUsers(Func<IQueryable<UserDTO.User>, IQueryable<UserDTO.User>>? conditions)
        {
            IQueryable<UserDTO.User> query = _databaseContext.Users.AsQueryable();
            if (conditions != null)
                query = conditions(query);

            IEnumerable<UserDomain.User> users =
                query
                .Select(userDTO => userDTO.ToDomain())
                .AsEnumerable();

            return await Task.FromResult(users);
        }

        public async Task<UserDomain.User?> GetUserByNameAsync(string firstName, string lastName)
        {
            var users = await GetUsers((query) =>
                                query.Where(user => user.FirstName == firstName && user.LastName == lastName));

            return await Task.FromResult(users.FirstOrDefault());
        }
    }
}

