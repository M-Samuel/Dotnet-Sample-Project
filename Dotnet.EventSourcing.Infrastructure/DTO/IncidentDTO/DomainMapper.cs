using System;
using System.ComponentModel.DataAnnotations;
using Dotnet.EventSourcing.Infrastructure.DTO.UserDTO;

namespace Dotnet.EventSourcing.Infrastructure.DTO.IncidentDTO
{
    public static class DomainMapper
    {
        public static IncidentDTO.IncidentStatusChange ToDTO(this Domain.IncidentDomain.IncidentStatusChange domainIncidentStatusChange)
        {
            IncidentDTO.IncidentStatusChange dtoIncidentStatusChange = new()
            {
                ChangedBy = domainIncidentStatusChange.ChangedBy?.ToDTO(),
                ChangedDateTime = domainIncidentStatusChange.ChangedDateTime,
                Id = domainIncidentStatusChange.Id,
                NewStatus = domainIncidentStatusChange.NewStatus,
                OldStatus = domainIncidentStatusChange.OldStatus
            };

            return dtoIncidentStatusChange;
        }


        public static Domain.IncidentDomain.IncidentStatusChange ToDomain(this IncidentDTO.IncidentStatusChange dtoIncidentStatusChange)
        {
            Domain.IncidentDomain.IncidentStatusChange incidentStatusChange = new()
            {
                Id = dtoIncidentStatusChange.Id,
                ChangedBy = dtoIncidentStatusChange.ChangedBy?.ToDomain(),
                OldStatus = dtoIncidentStatusChange.OldStatus,
                NewStatus = dtoIncidentStatusChange.NewStatus,
                ChangedDateTime = dtoIncidentStatusChange.ChangedDateTime
            };
            

            return incidentStatusChange;
        }



        public static IncidentDTO.Incident ToDTO(this Domain.IncidentDomain.Incident domainIncident)
        {
            IncidentDTO.Incident dtoIncident = new ()
            {
                Assignee = domainIncident.Assignee?.ToDTO(),
                CreatedDate = domainIncident.CreatedDate,
                Customer = domainIncident.Customer?.ToDTO(),
                Description = domainIncident.IncidentDetails?.Description,
                Id = domainIncident.Id,
                IncidentStatusChanges = domainIncident.IncidentStatusChanges.Select(isc => isc.ToDTO()).ToList(),
                Title = domainIncident.IncidentDetails?.Title,
                Status = domainIncident.Status
            };

            return dtoIncident;
        }


        public static Domain.IncidentDomain.Incident ToDomain(this IncidentDTO.Incident dtoIncident)
        {
            Domain.IncidentDomain.Incident domainIncident = new()
            {
                Assignee = dtoIncident.Assignee?.ToDomain(),
                CreatedDate = dtoIncident.CreatedDate,
                Customer = dtoIncident.Customer?.ToDomain(),
                Id = dtoIncident.Id,
                IncidentDetails = new(dtoIncident.Title ?? "", dtoIncident.Description ?? ""),
                IncidentStatusChanges = dtoIncident.IncidentStatusChanges.Select(isc => isc.ToDomain()).ToList(),
                Status = dtoIncident.Status
            };

            return domainIncident;
        }
    }

}