using System;
using Dotnet.EventSourcing.Domain.IncidentDomain;
using Dotnet.EventSourcing.SharedKernel;

namespace Dotnet.EventSourcing.Domain.IncidentDomian
{
	public interface IIncidentRepository
	{
		Task<Incident?> GetIncidentByIdAsync(Guid incidentId);
		Task CreateIncidentAsync(Incident incident);
		Task UpdateIncidentAsync(Incident incident);
	}
}

