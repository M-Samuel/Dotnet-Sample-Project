using System;
using System.ComponentModel.DataAnnotations;
using Dotnet.EventSourcing.Domain.IncidentDomain;
using Dotnet.EventSourcing.Infrastructure.DTO.UserDTO;

namespace Dotnet.EventSourcing.Infrastructure.DTO.IncidentDTO
{
    public class IncidentStatusChange
    {
        public Guid Id { get; set; }
        public User? ChangedBy { get; set; }
        public IncidentStatus OldStatus { get; set; }
        public IncidentStatus NewStatus { get; set; }
        public DateTime ChangedDateTime { get; set; }
    }

}