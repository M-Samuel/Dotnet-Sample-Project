

using Dotnet.EventSourcing.Domain.IncidentDomain;
using Dotnet.EventSourcing.Infrastructure.Contexts;
using Dotnet.EventSourcing.Infrastructure.DTO.IncidentDTO;

namespace Dotnet.EventSourcing.Infrastructure.Repositories
{
    public class IncidentRepository : IIncidentRepository
    {
        private readonly DatabaseContext _databaseContext;

        public IncidentRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }


        public async Task CreateIncidentAsync(Domain.IncidentDomain.Incident domainIncident)
        {
            var dtoIncident = domainIncident.ToDTO();
            await _databaseContext.Incidents.AddAsync(dtoIncident);
        }

        public async Task<Domain.IncidentDomain.Incident?> GetIncidentByIdAsync(Guid incidentId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateIncidentAsync(Domain.IncidentDomain.Incident incident)
        {
            throw new NotImplementedException();
        }
    }
}