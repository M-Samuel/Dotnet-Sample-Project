using System;
using Dotnet.EventSourcing.Domain.IncidentDomain;
using Dotnet.EventSourcing.SharedKernel;

namespace Dotnet.EventSourcing.Domain.UserDomain
{
    public partial class User
    {
        public ICollection<Incident> IncidentsAsAssignee { get; set; } = new List<Incident>();
        public ICollection<Incident> IncidentsAsCustomer { get; set; } = new List<Incident>();


        public ICollection<IncidentStatusChange> IncidentStatusChanges { get; set; } = new List<IncidentStatusChange>();
    }
}