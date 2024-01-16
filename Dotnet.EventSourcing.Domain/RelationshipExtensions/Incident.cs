using System;
using Dotnet.EventSourcing.Domain.UserDomain;
using Dotnet.EventSourcing.SharedKernel;

namespace Dotnet.EventSourcing.Domain.IncidentDomain
{
    public partial class Incident
    {
        public Guid? AssigneeId { get; set;}
        public Guid? CustomerId { get; set;}
    }

}