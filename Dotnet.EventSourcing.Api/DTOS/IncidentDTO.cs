using Dotnet.EventSourcing.Domain.IncidentDomain;
using Dotnet.EventSourcing.Domain.UserDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.EventSourcing.Api.DTOS
{
    public class IncidentDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public UserDTO? Customer { get; set; }
        public UserDTO? Assignee { get; set; }
        public string? Status { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<IncidentStatusChangeDTO> IncidentStatusChanges { get; set; } = new();
    }
}
