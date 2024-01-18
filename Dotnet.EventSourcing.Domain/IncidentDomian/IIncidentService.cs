using System;
using Dotnet.EventSourcing.Domain.IncidentDomain.IncidentDomainEvents;
using Dotnet.EventSourcing.SharedKernel;

namespace Dotnet.EventSourcing.Domain.IncidentDomain
{
	public interface IIncidentService
	{
        Task<Result<Incident>> ProcessDomainEvent(OpenIncidentEvent domainEvent, CancellationToken cancellationToken);
        Task<Result<Incident>> ProcessDomainEvent(AcknowledgeIncidentEvent domainEvent, CancellationToken cancellationToken);
        Task<Result<Incident>> ProcessDomainEvent(MoveIncidentToInProgressEvent domainEvent, CancellationToken cancellationToken);
        Task<Result<Incident>> ProcessDomainEvent(MoveIncidentToStandByEvent domainEvent, CancellationToken cancellationToken);
        Task<Result<Incident>> ProcessDomainEvent(ResumeIncidentEvent domainEvent, CancellationToken cancellationToken);
        Task<Result<Incident>> ProcessDomainEvent(CompleteIncidentEvent domainEvent, CancellationToken cancellationToken);
        Task<Result<Incident>> ProcessDomainEvent(ValidateIncidentEvent domainEvent, CancellationToken cancellationToken);
        Task<Result<Incident>> ProcessDomainEvent(ReOpenIncidentEvent domainEvent, CancellationToken cancellationToken);
        Task<Result<Incident>> ProcessDomainEvent(AssignIncidentEvent domainEvent, CancellationToken cancellationToken);

    }
}

