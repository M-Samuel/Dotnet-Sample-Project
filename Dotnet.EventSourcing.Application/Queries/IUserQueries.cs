using Dotnet.EventSourcing.Domain.UserDomain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.EventSourcing.Application.Queries
{
    public interface IUserQueries
    {
        Task<User?> GetUserByIdAsync(EventId eventId, Guid userId, CancellationToken cancellationToken);
        Task<User?> GetUserByNameAsync(EventId eventId, string firstName, string lastName, CancellationToken cancellationToken);
    }
}
