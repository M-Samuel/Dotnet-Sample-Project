using Dotnet.EventSourcing.Domain.IncidentDomain;
using Dotnet.EventSourcing.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Dotnet.EventSourcing.Application.Commands.ValidateIncident
{
    public interface IValidateIncidentCommand : ICommand<ValidateIncidentData,Result<Incident>>
    {
    }
}
