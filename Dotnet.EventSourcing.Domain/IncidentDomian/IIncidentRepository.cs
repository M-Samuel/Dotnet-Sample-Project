using System;
using Dotnet.EventSourcing.Domain.IncidentDomain;
using Dotnet.EventSourcing.SharedKernel;

namespace Dotnet.EventSourcing.Domain.IncidentDomain
{
	public interface IIncidentRepository
	{
		Task<Incident[]> GetAllIncidents(CancellationToken cancellationToken);
		Task<Incident?> GetIncidentByIdAsync(Guid incidentId, CancellationToken cancellationToken);
		Task CreateIncidentAsync(Incident incident, CancellationToken cancellationToken);
		void UpdateIncident(Incident incident);
	}
}

