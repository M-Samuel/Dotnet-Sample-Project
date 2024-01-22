using Dotnet.EventSourcing.Domain.IncidentDomain;
using Dotnet.EventSourcing.Domain.UserDomain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.EventSourcing.Application.Queries
{
    public class IncidentQueries : IIncidentQueries
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly ILogger<IncidentQueries> _logger;

        public IncidentQueries(IIncidentRepository incidentRepository, ILogger<IncidentQueries> logger)
        {
            _incidentRepository = incidentRepository;
            _logger = logger;
        }

        public async Task<Incident[]> GetAllIncidents(EventId eventId, CancellationToken cancellationToken)
        {
            _logger.LogInformation(eventId, $"{nameof(GetAllIncidents)} called");
            return await _incidentRepository.GetAllIncidents(cancellationToken);
        }

        public async Task<Incident?> GetIncidentByIdAsync(Guid incidentId, EventId eventId, CancellationToken cancellationToken)
        {
            _logger.LogInformation(eventId, $"{nameof(GetIncidentByIdAsync)} called with parameters: incidentId {incidentId}");
            return await _incidentRepository.GetIncidentByIdAsync(incidentId, cancellationToken);
        }
    }
}
