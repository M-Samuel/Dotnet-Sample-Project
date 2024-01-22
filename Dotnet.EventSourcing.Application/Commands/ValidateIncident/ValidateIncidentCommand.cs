using Dotnet.EventSourcing.Application.Commands.ResumeIncident;
using Dotnet.EventSourcing.Domain.IncidentDomain;
using Dotnet.EventSourcing.SharedKernel;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dotnet.EventSourcing.Application.Commands.ValidateIncident
{
    public class ValidateIncidentCommand : IValidateIncidentCommand
    {
        private readonly IIncidentService _incidentService;
        private readonly ILogger<ValidateIncidentCommand> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public ValidateIncidentCommand(IIncidentService incidentService, ILogger<ValidateIncidentCommand> logger, IUnitOfWork unitOfWork)
        {
            _incidentService = incidentService;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Incident>> ProcessCommandAsync(ValidateIncidentData commandData, EventId eventId, CancellationToken cancellationToken)
        {
            _logger.LogInformation(eventId, $"{nameof(ValidateIncidentCommand)} called with parameters: {JsonSerializer.Serialize(commandData)}");

            var result = await _incidentService.ProcessDomainEvent(commandData.ToEvent(), cancellationToken);

            if (!result.HasError)
                await _unitOfWork.SaveAsync(cancellationToken);

            return result;
        }
    }
}
