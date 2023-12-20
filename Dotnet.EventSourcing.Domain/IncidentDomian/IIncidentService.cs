using System;
using Dotnet.EventSourcing.Domain.CustomerDomain;
using Dotnet.EventSourcing.Domain.IncidentDomain;
using Dotnet.EventSourcing.Domain.IncidentDomian.IncidentDomainEvents;
using Dotnet.EventSourcing.SharedKernel;

namespace Dotnet.EventSourcing.Domain.IncidentDomian
{
	public interface IIncidentService
	{
        Task<Result<Incident>> ProcessDomainEvent(IncidentCreatedDomainEvent domainEvent);
        Task<Result<Incident>> ProcessDomainEvent(IncidentAcknowledgedDomainEvent domainEvent);
        Task<Result<Incident>> ProcessDomainEvent(IncidentInProgressDomainEvent domainEvent);
        Task<Result<Incident>> ProcessDomainEvent(IncidentResumeFromStandyDomainEvent domainEvent);
        Task<Result<Incident>> ProcessDomainEvent(IncidentStandByDomainEvent domainEvent);
        Task<Result<Incident>> ProcessDomainEvent(IncidentCompletedDomainEvent domainEvent);
        Task<Result<Incident>> ProcessDomainEvent(IncidentValidatedDomainEvent domainEvent);
        Task<Result<Incident>> ProcessDomainEvent(IncidentReOpenDomainEvent domainEvent);

    }
}

