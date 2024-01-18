using System;
using Dotnet.EventSourcing.Domain.IncidentDomain;
using Dotnet.EventSourcing.Domain.IncidentDomain.IncidentDomainEvents;
using Dotnet.EventSourcing.Domain.UserDomain;
using Dotnet.EventSourcing.SharedKernel;

namespace Dotnet.EventSourcing.Domain.IncidentDomain
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



        public async Task<Result<Incident>> ProcessDomainEvent(OpenIncidentEvent domainEvent, CancellationToken cancellationToken)
        {
            User? customer = await _userRepository.GetUserByIdAsync(domainEvent.CustomerId, cancellationToken);
            if (customer == null)
            {
                Result<Incident> resultError = Result<Incident>.Create();
                resultError.AddError(new UserNotFoundError($"User with id does not exists {domainEvent.CustomerId}"));
                return resultError;
            }
            Incident createdIncident = Incident.CreateNew(domainEvent.OccurranceDateTime, customer, domainEvent.Title, domainEvent.Description);
            createdIncident.UpdateStatus(IncidentStatus.Opened, customer);
            await _incidentRepository.CreateIncidentAsync(createdIncident, cancellationToken);
            Result<Incident> result = Result<Incident>.Create(createdIncident);

            return result;
        }

        private UserNotFoundError CreateUserNotExistsError(Guid userId)
        {
            return new UserNotFoundError($"User with id {userId} does not exists");
        }

        private IncidentNotFoundError CreateIncidentNotExistsError(Guid incidentId)
        {
            return new IncidentNotFoundError($"Incident with id {incidentId} does not exists");
        }


        public async Task<Result<Incident>> ProcessDomainEvent(AcknowledgeIncidentEvent domainEvent, CancellationToken cancellationToken)
        {
            Incident? incident = await _incidentRepository.GetIncidentByIdAsync(domainEvent.IncidentId, cancellationToken);
            User? changedBy = await _userRepository.GetUserByIdAsync(domainEvent.ChangedByUserId, cancellationToken);

            Result<Incident> result = Result<Incident>.Create();

            result
                .AddErrorIf(() => incident == null, CreateIncidentNotExistsError(domainEvent.IncidentId))
                .AddErrorIf(() => changedBy == null, CreateUserNotExistsError(domainEvent.ChangedByUserId))
                .AddErrorIf(() => incident != null && !incident.ValidateCanAcknowledge(), IncidentErrors.CannotAcknowledgeIncidentNotOpenedError)
                .AddErrorIf(() => incident != null && !incident.ValidateHasAssignee(), IncidentErrors.CannotAcknowledgeAsNoAssigneeError);

            if (result.HasError)
                return result;


            if (incident != null && changedBy != null)
            {
                incident.UpdateStatus(IncidentStatus.Acknowledged, changedBy);
                result.UpdateValueIfNoError(incident);
                _incidentRepository.UpdateIncident(incident);
            }


            return result;
        }

        public async Task<Result<Incident>> ProcessDomainEvent(AssignIncidentEvent domainEvent, CancellationToken cancellationToken)
        {
            Incident? incident = await _incidentRepository.GetIncidentByIdAsync(domainEvent.IncidentId, cancellationToken);
            User? assignee = await _userRepository.GetUserByIdAsync(domainEvent.AssigneeUserId, cancellationToken);


            Result<Incident> result = Result<Incident>.Create();

            result
                .AddErrorIf(() => incident == null, CreateIncidentNotExistsError(domainEvent.IncidentId))
                .AddErrorIf(() => assignee == null, CreateUserNotExistsError(domainEvent.AssigneeUserId));

            if (result.HasError)
                return result;

            if (incident != null && assignee != null)
            {
                incident.ChangeAssignee(assignee);
                result.UpdateValueIfNoError(incident);
                _incidentRepository.UpdateIncident(incident);
            }

            return result;
        }

        public async Task<Result<Incident>> ProcessDomainEvent(MoveIncidentToInProgressEvent domainEvent, CancellationToken cancellationToken)
        {
            Incident? incident = await _incidentRepository.GetIncidentByIdAsync(domainEvent.IncidentId, cancellationToken);
            User? changedBy = await _userRepository.GetUserByIdAsync(domainEvent.ChangedByUserId, cancellationToken);

            Result<Incident> result = Result<Incident>.Create();

            result
                .AddErrorIf(() => incident == null, CreateUserNotExistsError(domainEvent.IncidentId))
                .AddErrorIf(() => changedBy == null, CreateIncidentNotExistsError(domainEvent.ChangedByUserId))
                .AddErrorIf(() => incident != null && !incident.ValidateCanInProgress(), IncidentErrors.CannotInProgressIncidentNotAcknowledgeError);

            if (result.HasError)
                return result;

            if (incident != null && changedBy != null)
            {
                incident.UpdateStatus(IncidentStatus.InProgress, changedBy);
                result.UpdateValueIfNoError(incident);
                _incidentRepository.UpdateIncident(incident);
            }

            return result;
        }

        public async Task<Result<Incident>> ProcessDomainEvent(ResumeIncidentEvent domainEvent, CancellationToken cancellationToken)
        {
            Incident? incident = await _incidentRepository.GetIncidentByIdAsync(domainEvent.IncidentId, cancellationToken);
            User? changedBy = await _userRepository.GetUserByIdAsync(domainEvent.ChangedByUserId, cancellationToken);

            Result<Incident> result = Result<Incident>.Create();

            result
                .AddErrorIf(() => incident == null, CreateIncidentNotExistsError(domainEvent.IncidentId))
                .AddErrorIf(() => changedBy == null, CreateUserNotExistsError(domainEvent.ChangedByUserId))
                .AddErrorIf(() => incident != null && !incident.ValidateCanInProgress(), IncidentErrors.CannotInProgressIncidentNotStandByError);

            if (result.HasError)
                return result;

            if (incident != null && changedBy != null)
            {
                incident.UpdateStatus(IncidentStatus.InProgress, changedBy);
                result.UpdateValueIfNoError(incident);
                _incidentRepository.UpdateIncident(incident);
            }
            return result;
        }

        public async Task<Result<Incident>> ProcessDomainEvent(MoveIncidentToStandByEvent domainEvent, CancellationToken cancellationToken)
        {
            Incident? incident = await _incidentRepository.GetIncidentByIdAsync(domainEvent.IncidentId, cancellationToken);
            User? changedBy = await _userRepository.GetUserByIdAsync(domainEvent.ChangedByUserId, cancellationToken);


            Result<Incident> result = Result<Incident>.Create();

            result
                .AddErrorIf(() => incident == null, CreateIncidentNotExistsError(domainEvent.IncidentId))
                .AddErrorIf(() => changedBy == null, CreateUserNotExistsError(domainEvent.ChangedByUserId))
                .AddErrorIf(() => incident != null && !incident.ValidateCanStandy(), IncidentErrors.CannotStandByIncidentNotInProgressError);

            if (result.HasError)
                return result;

            if (incident != null && changedBy != null)
            {
                incident.UpdateStatus(IncidentStatus.StandBy, changedBy);
                result.UpdateValueIfNoError(incident);
                _incidentRepository.UpdateIncident(incident);
            }
            return result;
        }

        public async Task<Result<Incident>> ProcessDomainEvent(CompleteIncidentEvent domainEvent, CancellationToken cancellationToken)
        {
            Incident? incident = await _incidentRepository.GetIncidentByIdAsync(domainEvent.IncidentId, cancellationToken);
            User? changedBy = await _userRepository.GetUserByIdAsync(domainEvent.ChangedByUserId, cancellationToken);


            Result<Incident> result = Result<Incident>.Create();

            result
                .AddErrorIf(() => incident == null, CreateIncidentNotExistsError(domainEvent.IncidentId))
                .AddErrorIf(() => changedBy == null, CreateUserNotExistsError(domainEvent.ChangedByUserId))
                .AddErrorIf(() => incident != null && !incident.ValidateCanComplete(), IncidentErrors.CannotCompleteIncidentNotInProgressError);

            if (result.HasError)
                return result;

            if (incident != null && changedBy != null)
            {
                incident.UpdateStatus(IncidentStatus.Completed, changedBy);
                result.UpdateValueIfNoError(incident);
                _incidentRepository.UpdateIncident(incident);
            }

            return result;
        }

        public async Task<Result<Incident>> ProcessDomainEvent(ValidateIncidentEvent domainEvent, CancellationToken cancellationToken)
        {
            Incident? incident = await _incidentRepository.GetIncidentByIdAsync(domainEvent.IncidentId, cancellationToken);
            User? changedBy = await _userRepository.GetUserByIdAsync(domainEvent.ChangedByUserId, cancellationToken);


            Result<Incident> result = Result<Incident>.Create();

            result
                .AddErrorIf(() => incident == null, CreateIncidentNotExistsError(domainEvent.IncidentId))
                .AddErrorIf(() => changedBy == null, CreateUserNotExistsError(domainEvent.ChangedByUserId))
                .AddErrorIf(() => incident != null && !incident.ValidateCanClose(), IncidentErrors.CannotCloseIncidentNotCompletedError);

            if (result.HasError)
                return result;

            if (incident != null && changedBy != null)
            {
                incident.UpdateStatus(IncidentStatus.Closed, changedBy);
                result.UpdateValueIfNoError(incident);
                _incidentRepository.UpdateIncident(incident);
            }

            return result;
        }

        public async Task<Result<Incident>> ProcessDomainEvent(ReOpenIncidentEvent domainEvent, CancellationToken cancellationToken)
        {
            Incident? incident = await _incidentRepository.GetIncidentByIdAsync(domainEvent.IncidentId, cancellationToken);
            User? changedBy = await _userRepository.GetUserByIdAsync(domainEvent.ChangedByUserId, cancellationToken);

            Result<Incident> result = Result<Incident>.Create();

            result
                .AddErrorIf(() => incident == null, CreateIncidentNotExistsError(domainEvent.IncidentId))
                .AddErrorIf(() => changedBy == null, CreateUserNotExistsError(domainEvent.ChangedByUserId))
                .AddErrorIf(() => incident != null && !incident.ValidateCanReOpen(), IncidentErrors.CannotReOpenIncidentNotCompletedError);

            if (result.HasError)
                return result;

            if (incident != null && changedBy != null)
            {
                incident.UpdateStatus(IncidentStatus.Opened, changedBy);
                result.UpdateValueIfNoError(incident);
                _incidentRepository.UpdateIncident(incident);
            }

            return result;
        }
    }
}

