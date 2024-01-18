using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.EventSourcing.Domain.IncidentDomain
{
    public partial class IncidentStatusChange
    {
        public Incident? Incident { get; set; }
        public Guid? IncidentId { get; set; }

        public Guid? ChangedByUserId { get; set; }
    }
}
