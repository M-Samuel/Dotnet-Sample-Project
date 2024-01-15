using System;
using Dotnet.EventSourcing.Domain.IncidentDomain.IncidentDomainEvents;
using Dotnet.EventSourcing.SharedKernel;

namespace Dotnet.EventSourcing.Domain.IncidentDomain
{
	public interface IIncidentService
	{
        Task<Result<Incident>> ProcessDomainEvent(OpenIncidentEvent domainEvent);
        Task<Result<Incident>> ProcessDomainEvent(AcknowledgeIncidentEvent domainEvent);
        Task<Result<Incident>> ProcessDomainEvent(MoveIncidentToInProgressEvent domainEvent);
        Task<Result<Incident>> ProcessDomainEvent(MoveIncidentToStandByEvent domainEvent);
        Task<Result<Incident>> ProcessDomainEvent(ResumeIncidentEvent domainEvent);
        Task<Result<Incident>> ProcessDomainEvent(CompleteIncidentEvent domainEvent);
        Task<Result<Incident>> ProcessDomainEvent(ValidateIncidentEvent domainEvent);
        Task<Result<Incident>> ProcessDomainEvent(ReOpenIncidentEvent domainEvent);
        Task<Result<Incident>> ProcessDomainEvent(AssignIncidentEvent domainEvent);

    }
}

