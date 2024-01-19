

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


        public async Task CreateIncidentAsync(Incident incident, CancellationToken cancellationToken)
        {
            await _databaseContext.AddAsync(incident, cancellationToken);
        }

        public async Task<Incident?> GetIncidentByIdAsync(Guid incidentId, CancellationToken cancellationToken)
        {
            return await _databaseContext.Incidents
            .Include(i => i.Customer)
            .Include(i => i.Assignee)
            .Include(i => i.IncidentStatusChanges)
            .AsQueryable()
            .SingleOrDefaultAsync(incident => incident.Id == incidentId, cancellationToken);
        }

        public void UpdateIncident(Incident incident)
        {
            _databaseContext.Incidents.Update(incident);
            _databaseContext.IncidentStatusChanges.UpdateRange(incident.IncidentStatusChanges);
        }
    }
}