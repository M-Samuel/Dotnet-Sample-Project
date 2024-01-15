using System;
using Dotnet.EventSourcing.Domain.IncidentDomain;
using Dotnet.EventSourcing.SharedKernel;

namespace Dotnet.EventSourcing.Domain.IncidentDomain.IncidentDomainEvents
{

    public record OpenIncidentEvent(DateTime OccurranceDateTime, Guid CustomerId, IncidentDetails IncidentDetails) : IDomainEvent;
    public record AcknowledgeIncidentEvent(DateTime OccurranceDateTime, Guid IncidentId, Guid ChangedByUserId) : IDomainEvent;
    public record MoveIncidentToInProgressEvent(DateTime OccurranceDateTime, Guid IncidentId, Guid ChangedByUserId) : IDomainEvent;
    public record ResumeIncidentEvent(DateTime OccurranceDateTime, Guid IncidentId, Guid ChangedByUserId) : IDomainEvent;
    public record MoveIncidentToStandByEvent(DateTime OccurranceDateTime, Guid IncidentId, Guid ChangedByUserId) : IDomainEvent;
    public record CompleteIncidentEvent(DateTime OccurranceDateTime, Guid IncidentId, Guid ChangedByUserId) : IDomainEvent;
    public record ValidateIncidentEvent(DateTime OccurranceDateTime, Guid IncidentId, Guid ChangedByUserId) : IDomainEvent;
    public record ReOpenIncidentEvent(DateTime OccurranceDateTime, Guid IncidentId, Guid ChangedByUserId) : IDomainEvent;

    public record AssignIncidentEvent(DateTime OccurranceDateTime, Guid IncidentId, Guid AssigneeUserId) : IDomainEvent;


}

