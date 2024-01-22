using Dotnet.EventSourcing.Domain.IncidentDomain.IncidentDomainEvents;
using Dotnet.EventSourcing.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.EventSourcing.Application.Commands.ReOpenIncident
{
    public class ReOpenIncidentData : ICommandData<ReOpenIncidentEvent>
    {
        public Guid IncidentId { get; set; }
        public Guid ChangedByUserId { get; set; }

        public ReOpenIncidentEvent ToEvent()
        {
            return new ReOpenIncidentEvent(DateTime.UtcNow, IncidentId, ChangedByUserId);
        }
    }
}
