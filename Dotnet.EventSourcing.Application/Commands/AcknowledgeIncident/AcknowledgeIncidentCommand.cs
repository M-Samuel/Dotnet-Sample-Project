﻿using Dotnet.EventSourcing.Application.Commands.MoveIncidentToInProgress;
using Dotnet.EventSourcing.Application.Commands.OpenIncident;
using Dotnet.EventSourcing.Domain.IncidentDomain;
using Dotnet.EventSourcing.SharedKernel;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dotnet.EventSourcing.Application.Commands.AcknowledgeIncident
{
    public class AcknowledgeIncidentCommand : IAcknowledgeIncidentCommand
    {
        private readonly IIncidentService _incidentService;
        private readonly ILogger<OpenIncidentCommand> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public AcknowledgeIncidentCommand(IIncidentService incidentService, ILogger<OpenIncidentCommand> logger, IUnitOfWork unitOfWork)
        {
            _incidentService = incidentService;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Incident>> ProcessCommandAsync(AcknowledgeIncidentData commandData, EventId eventId, CancellationToken cancellationToken)
        {
            _logger.LogInformation(eventId, $"{nameof(AcknowledgeIncidentCommand)} called with parameters: {JsonSerializer.Serialize(commandData)}");

            var result = await _incidentService.ProcessDomainEvent(commandData.ToEvent(), cancellationToken);

            if (!result.HasError)
                await _unitOfWork.SaveAsync(cancellationToken);

            return result;
        }
    }
}
