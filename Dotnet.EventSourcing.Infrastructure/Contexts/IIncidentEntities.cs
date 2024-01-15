using System;
using Dotnet.EventSourcing.Infrastructure.DTO.IncidentDTO;
using Microsoft.EntityFrameworkCore;
namespace Dotnet.EventSourcing.Infrastructure.Contexts
{
	public interface IIncidentEntities
	{
		DbSet<Incident> Incidents { get; set; }
		DbSet<IncidentStatusChange> IncidentStatusChanges { get; set; }
	}
}

