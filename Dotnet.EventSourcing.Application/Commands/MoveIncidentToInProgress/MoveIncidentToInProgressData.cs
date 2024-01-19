using Dotnet.EventSourcing.Domain.IncidentDomain.IncidentDomainEvents;
using Dotnet.EventSourcing.SharedKernel;

namespace Dotnet.EventSourcing.Application.Commands.MoveIncidentToInProgress
{
    public class MoveIncidentToInProgressData : ICommandData<AssignIncidentEvent>
    {
        public Guid IncidentId { get; set; }
        public Guid ChangedByUserId { get; set; }


        public AssignIncidentEvent ToEvent()
        {
            return new AssignIncidentEvent(DateTime.UtcNow, IncidentId, ChangedByUserId);   
        }
    }
}