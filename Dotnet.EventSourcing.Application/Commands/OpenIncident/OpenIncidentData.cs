using Dotnet.EventSourcing.Domain.IncidentDomain.IncidentDomainEvents;
using Dotnet.EventSourcing.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.EventSourcing.Application.Commands.OpenIncident
{
    public class OpenIncidentData : ICommandData<OpenIncidentEvent>
    {

        public required Guid CustomerId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }

        public OpenIncidentEvent ToEvent()
        {
            return new OpenIncidentEvent(
                DateTime.UtcNow,
                CustomerId,
                Title,
                Description
            );
        }
    }
}
