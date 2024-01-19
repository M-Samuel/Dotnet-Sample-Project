using Dotnet.EventSourcing.Domain.IncidentDomain.IncidentDomainEvents;
using Dotnet.EventSourcing.SharedKernel;

namespace Dotnet.EventSourcing.Application.Commands.MoveIncidentToStandBy
{
    public class MoveIncidentToStandByData : ICommandData<MoveIncidentToStandByEvent>
    {
        public Guid IncidentId { get; set; }
        public Guid ChangedByUserId { get; set; }

        public MoveIncidentToStandByEvent ToEvent()
        {
            return new MoveIncidentToStandByEvent(DateTime.UtcNow, IncidentId, ChangedByUserId);
        }
    }
}