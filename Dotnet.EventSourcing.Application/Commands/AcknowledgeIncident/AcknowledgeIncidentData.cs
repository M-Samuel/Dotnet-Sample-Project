using Dotnet.EventSourcing.Domain.IncidentDomain.IncidentDomainEvents;
using Dotnet.EventSourcing.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.EventSourcing.Application.Commands.AcknowledgeIncident
{
    public class AcknowledgeIncidentData : ICommandData<AcknowledgeIncidentEvent>
    {
        public Guid IncidentId { get; set; }
        public Guid ChangedByUserId { get; set; }

        public AcknowledgeIncidentEvent ToEvent()
        {
            return new AcknowledgeIncidentEvent(DateTime.UtcNow, IncidentId, ChangedByUserId);
        }
    }
}
