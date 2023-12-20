using System;
using Dotnet.EventSourcing.Domain.CustomerDomain;
using Dotnet.EventSourcing.Domain.IncidentDomain;
using Dotnet.EventSourcing.Domain.IncidentDomian.IncidentDomainEvents;
using Dotnet.EventSourcing.SharedKernel;

namespace Dotnet.EventSourcing.Domain.IncidentDomian
{
    public class IncidentService
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly ICustomerRepository _customerRepository;

        public IncidentService(
            IIncidentRepository incidentRepository,
            ICustomerRepository customerRepository
            )
        {
            _incidentRepository = incidentRepository;
            _customerRepository = customerRepository;
        }

        public async Task<Result<Incident>> ProcessDomainEvent(IncidentCreatedDomainEvent domainEvent)
        {
            Customer customer = await _customerRepository.GetCustomerById(domainEvent.CustomerId);
            Incident createdIncident = Incident.CreateNew(domainEvent.OccurranceDateTime, customer, domainEvent.IncidentDetails);
            Result<Incident> result = Result<Incident>.Create(createdIncident);

            return result;
        }


        public async Task<Result<Incident>> ProcessDomainEvent(IncidentAcknowledgedDomainEvent domainEvent)
        {
            Incident incident = await _incidentRepository.GetIncidentById(domainEvent.IncidentId);
            Result<Incident> result = Result<Incident>.Create(incident);
            if (!incident.ValidateCanAcknowledge())
            {
                result.AddError(IncidentErrors.CannotAcknowledgeIncidentNotOpenedError);
                return result;
            }

            incident.UpdateStatus(IncidentStatus.Acknowledged);
            return result;
        }

        public async Task<Result<Incident>> ProcessDomainEvent(IncidentInProgressDomainEvent domainEvent)
        {
            Incident incident = await _incidentRepository.GetIncidentById(domainEvent.IncidentId);
            Result<Incident> result = Result<Incident>.Create(incident);
            if (!incident.ValidateCanInProgress())
            {
                result.AddError(IncidentErrors.CannotInProgressIncidentNotAcknowledgeError);
                return result;
            }

            incident.UpdateStatus(IncidentStatus.InProgress);
            return result;
        }

        public async Task<Result<Incident>> ProcessDomainEvent(IncidentResumeFromStandyDomainEvent domainEvent)
        {
            Incident incident = await _incidentRepository.GetIncidentById(domainEvent.IncidentId);
            Result<Incident> result = Result<Incident>.Create(incident);
            if (!incident.ValidateCanInProgress())
            {
                result.AddError(IncidentErrors.CannotInProgressIncidentNotStandByError);
                return result;
            }

            incident.UpdateStatus(IncidentStatus.InProgress);
            return result;
        }

        public async Task<Result<Incident>> ProcessDomainEvent(IncidentStandByDomainEvent domainEvent)
        {
            Incident incident = await _incidentRepository.GetIncidentById(domainEvent.IncidentId);
            Result<Incident> result = Result<Incident>.Create(incident);
            if (!incident.ValidateCanStandy())
            {
                result.AddError(IncidentErrors.CannotStandByIncidentNotInProgressError);
                return result;
            }

            incident.UpdateStatus(IncidentStatus.StandBy);
            return result;
        }

        public async Task<Result<Incident>> ProcessDomainEvent(IncidentCompletedDomainEvent domainEvent)
        {
            Incident incident = await _incidentRepository.GetIncidentById(domainEvent.IncidentId);
            Result<Incident> result = Result<Incident>.Create(incident);
            if (!incident.ValidateCanComplete())
            {
                result.AddError(IncidentErrors.CannotCompleteIncidentNotInProgressError);
                return result;
            }

            incident.UpdateStatus(IncidentStatus.Completed);
            return result;
        }

        public async Task<Result<Incident>> ProcessDomainEvent(IncidentValidatedDomainEvent domainEvent)
        {
            Incident incident = await _incidentRepository.GetIncidentById(domainEvent.IncidentId);
            Result<Incident> result = Result<Incident>.Create(incident);
            if (!incident.ValidateCanClose())
            {
                result.AddError(IncidentErrors.CannotCloseIncidentNotCompletedError);
                return result;
            }

            incident.UpdateStatus(IncidentStatus.Closed);
            return result;
        }

        public async Task<Result<Incident>> ProcessDomainEvent(IncidentReOpenDomainEvent domainEvent)
        {
            Incident incident = await _incidentRepository.GetIncidentById(domainEvent.IncidentId);
            Result<Incident> result = Result<Incident>.Create(incident);
            if (!incident.ValidateCanReOpen())
            {
                result.AddError(IncidentErrors.CannotReOpenIncidentNotCompletedError);
                return result;
            }

            incident.UpdateStatus(IncidentStatus.Opened);
            return result;
        }
    }
}

