using System;
using Dotnet.EventSourcing.Domain.IncidentDomain;
using Dotnet.EventSourcing.Domain.UserDomain;
using Dotnet.EventSourcing.SharedKernel;

namespace Dotnet.EventSourcing.Domain.IncidentDomian
{
	public record IncidentStatusChange(
        Guid Id,
        User ChangedBy,
        IncidentStatus OldStatus,
        IncidentStatus NewStatus,
        DateTime ChangedDateTime
    ) : IEntity;
	
}

