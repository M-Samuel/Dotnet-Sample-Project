using System;
using System.ComponentModel.DataAnnotations;
using Dotnet.EventSourcing.Domain.IncidentDomain;
using Dotnet.EventSourcing.Infrastructure.DTO.UserDTO;

namespace Dotnet.EventSourcing.Infrastructure.DTO.IncidentDTO
{
    public class Incident
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public User? Customer { get; set; }
        public User? Assignee { get; set; }
        public string? Title { get; set; }
        public string? Description {get;set;}
        public IncidentStatus Status { get; set; }
        public List<IncidentStatusChange> IncidentStatusChanges { get; set; } = new();
    }
}