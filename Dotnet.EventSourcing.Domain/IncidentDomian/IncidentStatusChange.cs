using System;
using Dotnet.EventSourcing.Domain.IncidentDomain;
using Dotnet.EventSourcing.Domain.UserDomain;
using Dotnet.EventSourcing.SharedKernel;

namespace Dotnet.EventSourcing.Domain.IncidentDomain
{
    public partial class IncidentStatusChange : IEntity
    {
        public Guid Id { get; set; }
        public User? ChangedBy { get; set; }
        public IncidentStatus OldStatus { get; set; }
        public IncidentStatus NewStatus { get; set; }
        public DateTime ChangedDateTime { get; set; }
    }
	
}

