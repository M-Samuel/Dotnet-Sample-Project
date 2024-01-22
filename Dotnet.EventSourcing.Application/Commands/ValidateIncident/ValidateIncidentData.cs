using Dotnet.EventSourcing.Domain.IncidentDomain.IncidentDomainEvents;
using Dotnet.EventSourcing.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.EventSourcing.Application.Commands.ValidateIncident
{
    public class ValidateIncidentData : ICommandData<ValidateIncidentEvent>
    {
        public Guid IncidentId { get; set; }
        public Guid ChangedByUserId { get; set; }

        public ValidateIncidentEvent ToEvent()
        {
            return new ValidateIncidentEvent(DateTime.UtcNow, IncidentId, ChangedByUserId);
        }
    }
}
