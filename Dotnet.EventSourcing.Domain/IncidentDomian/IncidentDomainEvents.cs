using System;
using Dotnet.EventSourcing.Domain.CustomerDomain;
using Dotnet.EventSourcing.Domain.IncidentDomain;
using Dotnet.EventSourcing.SharedKernel;

namespace Dotnet.EventSourcing.Domain.IncidentDomian.IncidentDomainEvents
{
	public record IncidentCreatedDomainEvent(DateTime OccurranceDateTime, Guid IncidentId, Guid CustomerId, IncidentDetails IncidentDetails) : IDomainEvent;
    public record IncidentAcknowledgedDomainEvent(DateTime OccurranceDateTime, Guid IncidentId) : IDomainEvent;
    public record IncidentInProgressDomainEvent(DateTime OccurranceDateTime, Guid IncidentId) : IDomainEvent;
    public record IncidentResumeFromStandyDomainEvent(DateTime OccurranceDateTime, Guid IncidentId) : IDomainEvent;
    public record IncidentStandByDomainEvent(DateTime OccurranceDateTime, Guid IncidentId) : IDomainEvent;
    public record IncidentCompletedDomainEvent(DateTime OccurranceDateTime, Guid IncidentId) : IDomainEvent;
    public record IncidentValidatedDomainEvent(DateTime OccurranceDateTime, Guid IncidentId) : IDomainEvent;
    public record IncidentReOpenDomainEvent(DateTime OccurranceDateTime, Guid IncidentId) : IDomainEvent;

}

