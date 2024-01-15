using System;
using System.Linq;
using Dotnet.EventSourcing.Domain.UserDomain;
using Dotnet.EventSourcing.Infrastructure.Contexts;
using UserDTO = Dotnet.EventSourcing.Infrastructure.DTO.UserDTO;
using UserDomain = Dotnet.EventSourcing.Domain.UserDomain;
using Dotnet.EventSourcing.Infrastructure.DTO.UserDTO;
using Dotnet.EventSourcing.SharedKernel;
using Microsoft.EntityFrameworkCore;

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
            var dtoUser = await _databaseContext.Users
                .AsQueryable()
                .SingleOrDefaultAsync(u => u.Id == userId);

            if(dtoUser == null) return null;

            return dtoUser.ToDomain();
        }

        public async Task<UserDomain.User?> GetUserByNameAsync(string firstName, string lastName)
        {

            var dtoUser = await _databaseContext.Users
                .AsQueryable()
                .SingleOrDefaultAsync(user => user.FirstName == firstName && user.LastName == lastName);

            if(dtoUser == null) return null;

            return dtoUser.ToDomain();
        }
    }
}

