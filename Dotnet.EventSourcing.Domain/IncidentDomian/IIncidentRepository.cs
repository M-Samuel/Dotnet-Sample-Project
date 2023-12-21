using System;
using Dotnet.EventSourcing.Domain.IncidentDomain;

namespace Dotnet.EventSourcing.Domain.IncidentDomian
{
	public interface IIncidentRepository
	{
		Task<Incident?> GetIncidentById(Guid incidentId);
	}
}

