using Dotnet.EventSourcing.Domain.IncidentDomain;
using Dotnet.EventSourcing.Domain.UserDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.EventSourcing.Api.DTOS
{
    public static class DTOMapper
    {
        public static UserDTO ToDTO(this User user)
        {
            return new UserDTO()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
            };
        }

        public static IncidentDTO ToDTO(this Incident incident)
        {
            return new IncidentDTO()
            {
                Assignee = incident.Assignee?.ToDTO(),
                CreatedDate = incident.CreatedDate,
                Customer = incident.Customer?.ToDTO(),
                Description = incident.Description,
                Id = incident.Id,
                IncidentStatusChanges = incident.IncidentStatusChanges.Select(isc => isc.ToDTO()).ToList(),
                Status = incident.Status.ToString(),
                Title = incident.Title
            };
        }

        public static IncidentStatusChangeDTO ToDTO(this IncidentStatusChange incidentStatusChange)
        {
            return new IncidentStatusChangeDTO()
            {
                ChangedBy = incidentStatusChange.ChangedBy?.ToDTO(),
                ChangedDateTime = incidentStatusChange.ChangedDateTime,
                Id = incidentStatusChange.Id,
                NewStatus = incidentStatusChange.NewStatus.ToString(),
                OldStatus = incidentStatusChange.OldStatus.ToString()
            };
        }
    }
}
