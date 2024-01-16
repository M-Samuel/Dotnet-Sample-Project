using System;
using Dotnet.EventSourcing.Domain.IncidentDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Dotnet.EventSourcing.Infrastructure.Contexts
{
	public interface IIncidentEntities
	{
		
		DbSet<Incident> Incidents { get; set; }
		void IncidentBuilder(EntityTypeBuilder<Incident> builder);
		// DbSet<IncidentStatusChange> IncidentStatusChanges { get; set; }
	}
}

