﻿using Dotnet.EventSourcing.Application.Commands.OpenIncident;
using Dotnet.EventSourcing.Domain.IncidentDomain;
using Dotnet.EventSourcing.SharedKernel;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dotnet.EventSourcing.Application.Commands.ResumeIncident
{
    public class ResumeIncidentCommand : IResumeIncidentCommand
    {
        private readonly IIncidentService _incidentService;
        private readonly ILogger<ResumeIncidentCommand> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public ResumeIncidentCommand(IIncidentService incidentService, ILogger<ResumeIncidentCommand> logger, IUnitOfWork unitOfWork)
        {
            _incidentService = incidentService;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Incident>> ProcessCommandAsync(ResumeIncidentData commandData, EventId eventId, CancellationToken cancellationToken)
        {
            _logger.LogInformation(eventId, $"{nameof(ResumeIncidentCommand)} called with parameters: {JsonSerializer.Serialize(commandData)}");

            var result = await _incidentService.ProcessDomainEvent(commandData.ToEvent(), cancellationToken);

            if (!result.HasError)
                await _unitOfWork.SaveAsync(cancellationToken);

            return result;
        }
    }
}
