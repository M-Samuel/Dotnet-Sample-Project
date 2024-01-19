using Dotnet.EventSourcing.Domain.IncidentDomain;
using Dotnet.EventSourcing.SharedKernel;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.EventSourcing.Application.Commands.AssignIncident
{
    public interface IAssignIncidentCommand : ICommand<AssignIncidentData, Result<Incident>>
    {
        
    }
}
