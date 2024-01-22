using Dotnet.EventSourcing.Domain.IncidentDomain.IncidentDomainEvents;
using Dotnet.EventSourcing.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.EventSourcing.Application.Commands.CompleteIncident
{
    public class CompleteIncidentData : ICommandData<CompleteIncidentEvent>
    {
        public Guid IncidentId { get; set; }
        public Guid ChangedByUserId { get; set; }

        public CompleteIncidentEvent ToEvent()
        {
            return new CompleteIncidentEvent(DateTime.UtcNow, IncidentId, ChangedByUserId);
        }
    }
}
