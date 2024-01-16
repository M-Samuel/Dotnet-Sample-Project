

using Dotnet.EventSourcing.Domain.IncidentDomain;
using Dotnet.EventSourcing.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.EventSourcing.Infrastructure.Repositories
{
    public class IncidentRepository : IIncidentRepository
    {
        private readonly DatabaseContext _databaseContext;

        public IncidentRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }


        public async Task CreateIncidentAsync(Incident incident)
        {
            await _databaseContext.Incidents.AddAsync(incident);
        }

        public async Task<Incident?> GetIncidentByIdAsync(Guid incidentId)
        {
            return await _databaseContext.Incidents
            .AsQueryable()
            .SingleOrDefaultAsync(incident => incident.Id == incidentId);
        }

        public async Task UpdateIncidentAsync(Incident incident)
        {
            _databaseContext.Incidents.Update(incident);
            await Task.CompletedTask;
        }
    }
}