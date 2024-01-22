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

namespace Dotnet.EventSourcing.Application.Commands.ReOpenIncident
{
    public class ReOpenIncidentCommand : IReOpenIncidentCommand
    {
        private readonly IIncidentService _incidentService;
        private readonly ILogger<ReOpenIncidentCommand> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public ReOpenIncidentCommand(IIncidentService incidentService, ILogger<ReOpenIncidentCommand> logger, IUnitOfWork unitOfWork)
        {
            _incidentService = incidentService;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Incident>> ProcessCommandAsync(ReOpenIncidentData commandData, EventId eventId, CancellationToken cancellationToken)
        {
            _logger.LogInformation(eventId, $"{nameof(ReOpenIncidentCommand)} called with parameters: {JsonSerializer.Serialize(commandData)}");

            var result = await _incidentService.ProcessDomainEvent(commandData.ToEvent(), cancellationToken);

            if (!result.HasError)
                await _unitOfWork.SaveAsync(cancellationToken);

            return result;
        }
    }
}
