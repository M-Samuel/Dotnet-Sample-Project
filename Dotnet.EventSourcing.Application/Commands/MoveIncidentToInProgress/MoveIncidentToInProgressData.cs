using Dotnet.EventSourcing.Domain.IncidentDomain.IncidentDomainEvents;
using Dotnet.EventSourcing.SharedKernel;

namespace Dotnet.EventSourcing.Application.Commands.MoveIncidentToInProgress
{
    public class MoveIncidentToInProgressData : ICommandData<MoveIncidentToInProgressEvent>
    {
        public Guid IncidentId { get; set; }
        public Guid ChangedByUserId { get; set; }


        public MoveIncidentToInProgressEvent ToEvent()
        {
            return new MoveIncidentToInProgressEvent(DateTime.UtcNow, IncidentId, ChangedByUserId);   
        }
    }
}