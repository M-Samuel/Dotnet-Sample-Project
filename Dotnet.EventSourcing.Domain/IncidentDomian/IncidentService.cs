using System;
using Dotnet.EventSourcing.Domain.IncidentDomain;
using Dotnet.EventSourcing.Domain.IncidentDomian.IncidentDomainEvents;
using Dotnet.EventSourcing.Domain.UserDomain;
using Dotnet.EventSourcing.SharedKernel;

namespace Dotnet.EventSourcing.Domain.IncidentDomian
{
    public class IncidentService : IIncidentService
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly IUserRepository _userRepository;

        public IncidentService(
            IIncidentRepository incidentRepository,
            IUserRepository userRepository
            )
        {
            _incidentRepository = incidentRepository;
            _userRepository = userRepository;
        }

        public async Task<Result<Incident>> ProcessDomainEvent(OpenIncidentEvent domainEvent)
        {
            User customer = await _userRepository.GetUserById(domainEvent.CustomerId);
            Incident createdIncident = Incident.CreateNew(domainEvent.OccurranceDateTime, customer, domainEvent.IncidentDetails);
            createdIncident.UpdateStatus(IncidentStatus.Opened, customer);
            Result<Incident> result = Result<Incident>.Create(createdIncident);

            return result;
        }


        public async Task<Result<Incident>> ProcessDomainEvent(AcknowledgeIncidentEvent domainEvent)
        {
            Incident incident = await _incidentRepository.GetIncidentById(domainEvent.IncidentId);
            User changedBy = await _userRepository.GetUserById(domainEvent.ChangedByUserId);
            Result<Incident> result = Result<Incident>.Create(incident);
            if (!incident.ValidateCanAcknowledge())
                result.AddError(IncidentErrors.CannotAcknowledgeIncidentNotOpenedError);
            if (!incident.ValidateHasAssignee())
                result.AddError(IncidentErrors.CannotAcknowledgeAsNoAssigneeError);

            if (result.HasError)
                return result;

            incident.UpdateStatus(IncidentStatus.Acknowledged, changedBy);
            return result;
        }

        public async Task<Result<Incident>> ProcessDomainEvent(AssignIncidentEvent domainEvent)
        {
            Incident incident = await _incidentRepository.GetIncidentById(domainEvent.IncidentId);
            User assignee = await _userRepository.GetUserById(domainEvent.AssigneeUserId);
            Result<Incident> result = Result<Incident>.Create(incident);
            incident.ChangeAssignee(assignee);
            return result;
        }

        public async Task<Result<Incident>> ProcessDomainEvent(MoveIncidentToInProgressEvent domainEvent)
        {
            Incident incident = await _incidentRepository.GetIncidentById(domainEvent.IncidentId);
            User changedBy = await _userRepository.GetUserById(domainEvent.ChangedByUserId);
            Result<Incident> result = Result<Incident>.Create(incident);
            if (!incident.ValidateCanInProgress())
            {
                result.AddError(IncidentErrors.CannotInProgressIncidentNotAcknowledgeError);
                return result;
            }

            incident.UpdateStatus(IncidentStatus.InProgress, changedBy);
            return result;
        }

        public async Task<Result<Incident>> ProcessDomainEvent(ResumeIncidentEvent domainEvent)
        {
            Incident incident = await _incidentRepository.GetIncidentById(domainEvent.IncidentId);
            User changedBy = await _userRepository.GetUserById(domainEvent.ChangedByUserId);
            Result<Incident> result = Result<Incident>.Create(incident);
            if (!incident.ValidateCanInProgress())
            {
                result.AddError(IncidentErrors.CannotInProgressIncidentNotStandByError);
                return result;
            }

            incident.UpdateStatus(IncidentStatus.InProgress, changedBy);
            return result;
        }

        public async Task<Result<Incident>> ProcessDomainEvent(MoveIncidentToStandByEvent domainEvent)
        {
            Incident incident = await _incidentRepository.GetIncidentById(domainEvent.IncidentId);
            User changedBy = await _userRepository.GetUserById(domainEvent.ChangedByUserId);
            Result<Incident> result = Result<Incident>.Create(incident);
            if (!incident.ValidateCanStandy())
            {
                result.AddError(IncidentErrors.CannotStandByIncidentNotInProgressError);
                return result;
            }

            incident.UpdateStatus(IncidentStatus.StandBy, changedBy);
            return result;
        }

        public async Task<Result<Incident>> ProcessDomainEvent(CompleteIncidentEvent domainEvent)
        {
            Incident incident = await _incidentRepository.GetIncidentById(domainEvent.IncidentId);
            User changedBy = await _userRepository.GetUserById(domainEvent.ChangedByUserId);
            Result<Incident> result = Result<Incident>.Create(incident);
            if (!incident.ValidateCanComplete())
            {
                result.AddError(IncidentErrors.CannotCompleteIncidentNotInProgressError);
                return result;
            }

            incident.UpdateStatus(IncidentStatus.Completed, changedBy);
            return result;
        }

        public async Task<Result<Incident>> ProcessDomainEvent(ValidateIncidentEvent domainEvent)
        {
            Incident incident = await _incidentRepository.GetIncidentById(domainEvent.IncidentId);
            User changedBy = await _userRepository.GetUserById(domainEvent.ChangedByUserId);
            Result<Incident> result = Result<Incident>.Create(incident);
            if (!incident.ValidateCanClose())
            {
                result.AddError(IncidentErrors.CannotCloseIncidentNotCompletedError);
                return result;
            }

            incident.UpdateStatus(IncidentStatus.Closed, changedBy);
            return result;
        }

        public async Task<Result<Incident>> ProcessDomainEvent(ReOpenIncidentEvent domainEvent)
        {
            Incident incident = await _incidentRepository.GetIncidentById(domainEvent.IncidentId);
            User changedBy = await _userRepository.GetUserById(domainEvent.ChangedByUserId);
            Result<Incident> result = Result<Incident>.Create(incident);
            if (!incident.ValidateCanReOpen())
            {
                result.AddError(IncidentErrors.CannotReOpenIncidentNotCompletedError);
                return result;
            }

            incident.UpdateStatus(IncidentStatus.Opened, changedBy);
            return result;
        }
    }
}

