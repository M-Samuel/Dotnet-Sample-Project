﻿using Dotnet.EventSourcing.Domain.IncidentDomain;
using Dotnet.EventSourcing.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.EventSourcing.Application.Commands.MoveIncidentToStandBy
{
    public interface IMoveIncidentToStandByCommand : ICommand<MoveIncidentToStandByData,Result<Incident>>
    {
    }
}
