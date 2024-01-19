using Dotnet.EventSourcing.Domain.IncidentDomain.IncidentDomainEvents;
using Dotnet.EventSourcing.SharedKernel;

namespace Dotnet.EventSourcing.Application.Commands.AssignIncident
{
    public class AssignIncidentData : ICommandData<AssignIncidentEvent>
    {
        public Guid IncidentId { get; set; }
        public Guid AssigneeUserId { get; set; }

        public AssignIncidentEvent ToEvent()
        {
            return new AssignIncidentEvent(DateTime.UtcNow, IncidentId, AssigneeUserId);
        }
    }
}