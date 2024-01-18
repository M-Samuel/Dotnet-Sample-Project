using Dotnet.EventSourcing.Domain.UserDomain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.EventSourcing.Application.Queries
{
    public class UserQueries : IUserQueries
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserQueries> _logger;

        public UserQueries(IUserRepository userRepository, ILogger<UserQueries> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<User?> GetUserByIdAsync(EventId eventId, Guid userId, CancellationToken cancellationToken)
        {
            _logger.LogInformation(eventId, $"{GetUserByIdAsync} called with parameters: userId {userId}");
            return await _userRepository.GetUserByIdAsync(userId, cancellationToken);
        }

        public async Task<User?> GetUserByNameAsync(EventId eventId, string firstName, string lastName, CancellationToken cancellationToken)
        {
            _logger.LogInformation(eventId, $"{GetUserByNameAsync} called with parameters: firstName {firstName}, lastName: {lastName}");
            return await _userRepository.GetUserByNameAsync(firstName, lastName, cancellationToken);
        }
    }
}
