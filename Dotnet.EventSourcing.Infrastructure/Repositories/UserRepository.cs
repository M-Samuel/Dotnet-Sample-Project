using System;
using Dotnet.EventSourcing.Domain.UserDomain;
using Dotnet.EventSourcing.Infrastructure.Contexts;
using UserDTO = Dotnet.EventSourcing.Infrastructure.DTO.UserDTO;
using UserDomain = Dotnet.EventSourcing.Domain.UserDomain;
using Dotnet.EventSourcing.Infrastructure.DTO.UserDTO;

namespace Dotnet.EventSourcing.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _databaseContext;

        public UserRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task CreateUser(UserDomain.User user)
        {
            UserDTO.User userDTO = user.ToDTO();
            await _databaseContext.AddAsync(userDTO);
        }

        public async Task<UserDomain.User?> GetUserById(Guid userId)
        {
            UserDTO.User? userDTO = _databaseContext.Users.FirstOrDefault(user => user.Id == userId);
            if (userDTO == null)
                return await Task.FromResult<UserDomain.User?>(null);

            return await Task.FromResult<UserDomain.User?>(userDTO.ToDomain());
        }
    }
}

