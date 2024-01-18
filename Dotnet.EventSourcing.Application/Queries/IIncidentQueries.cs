using Dotnet.EventSourcing.Domain.IncidentDomain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.EventSourcing.Application.Queries
{
    public interface IIncidentQueries
    {
        Task<Incident?> GetIncidentByIdAsync(Guid incidentId, EventId eventId, CancellationToken cancellationToken);
    }
}
