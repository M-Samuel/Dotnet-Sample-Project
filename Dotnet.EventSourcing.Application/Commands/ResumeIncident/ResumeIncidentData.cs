using Dotnet.EventSourcing.Domain.IncidentDomain.IncidentDomainEvents;
using Dotnet.EventSourcing.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.EventSourcing.Application.Commands.ResumeIncident
{
    public class ResumeIncidentData : ICommandData<ResumeIncidentEvent>
    {
        public Guid IncidentId { get; set; }
        public Guid ChangedByUserId { get; set; }

        public ResumeIncidentEvent ToEvent()
        {
            return new ResumeIncidentEvent(DateTime.UtcNow, IncidentId, ChangedByUserId);
        }
    }
}
