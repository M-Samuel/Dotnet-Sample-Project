using Dotnet.EventSourcing.Api.DTOS;
using Dotnet.EventSourcing.Application.Commands.AcknowledgeIncident;
using Dotnet.EventSourcing.Application.Commands.AssignIncident;
using Dotnet.EventSourcing.Application.Commands.CompleteIncident;
using Dotnet.EventSourcing.Application.Commands.MoveIncidentToInProgress;
using Dotnet.EventSourcing.Application.Commands.MoveIncidentToStandBy;
using Dotnet.EventSourcing.Application.Commands.OpenIncident;
using Dotnet.EventSourcing.Application.Commands.ReOpenIncident;
using Dotnet.EventSourcing.Application.Commands.ResumeIncident;
using Dotnet.EventSourcing.Application.Commands.ValidateIncident;
using Dotnet.EventSourcing.Application.Queries;
using Dotnet.EventSourcing.Domain.IncidentDomain;
using Dotnet.EventSourcing.SharedKernel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using System.Threading;

namespace Dotnet.EventSourcing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentController : ControllerBase
    {
        private readonly IIncidentQueries _incidentQueries;
        private readonly IOpenIncidentCommand _openIncidentCommand;
        private readonly IAssignIncidentCommand _assignIncidentCommand;
        private readonly IMoveIncidentToInProgressCommand _moveIncidentToInProgressCommand;
        private readonly IMoveIncidentToStandByCommand _moveIncidentToStandByCommand;
        private readonly IAcknowledgeIncidentCommand _acknowledgeIncidentCommand;
        private readonly IValidateIncidentCommand _validateIncidentCommand;
        private readonly ICompleteIncidentCommand _completeIncidentCommand;
        private readonly IReOpenIncidentCommand _reOpenIncidentCommand;
        private readonly IResumeIncidentCommand _resumeIncidentCommand;

        public IncidentController(IIncidentQueries incidentQueries
            ,IOpenIncidentCommand openIncidentCommand
            ,IAssignIncidentCommand assignIncidentCommand
            ,IMoveIncidentToInProgressCommand moveIncidentToInProgressCommand
            ,IMoveIncidentToStandByCommand moveIncidentToStandByCommand
            ,IAcknowledgeIncidentCommand acknowledgeIncidentCommand
            , IValidateIncidentCommand validateIncidentCommand
            , ICompleteIncidentCommand completeIncidentCommand
            , IReOpenIncidentCommand reOpenIncidentCommand
            , IResumeIncidentCommand resumeIncidentCommand)
        {
            _incidentQueries = incidentQueries;
            _openIncidentCommand = openIncidentCommand;
            _assignIncidentCommand = assignIncidentCommand;
            _moveIncidentToInProgressCommand = moveIncidentToInProgressCommand;
            _moveIncidentToStandByCommand = moveIncidentToStandByCommand;
            _acknowledgeIncidentCommand = acknowledgeIncidentCommand;
            _validateIncidentCommand = validateIncidentCommand;
            _completeIncidentCommand = completeIncidentCommand;
            _reOpenIncidentCommand = reOpenIncidentCommand;
            _resumeIncidentCommand = resumeIncidentCommand;
        }


        [HttpGet("get/{id}")]
        public async Task<ActionResult<IncidentDTO>> GetIncidentById(Guid id, CancellationToken cancellationToken)
        {
            EventId eventId = new EventId(0, Guid.NewGuid().ToString());
            var incident = await _incidentQueries.GetIncidentByIdAsync(id, eventId, cancellationToken);
            if (incident == null)
                return NotFound();
            return Ok(incident.ToDTO());
        }

        [HttpPost("open")]

        public async Task<ActionResult<IncidentDTO>> CreateUser([FromForm] OpenIncidentData formData, CancellationToken cancellationToken)
        {
            EventId eventId = new EventId(0, Guid.NewGuid().ToString());
            Result<Incident> result = await _openIncidentCommand.ProcessCommandAsync(formData, eventId, cancellationToken);

            if (result.HasError)
                return BadRequest(result.DomainErrors);

            return new CreatedAtActionResult(nameof(GetIncidentById), "Incident", new { id = result.EntityValue.Id }, result.EntityValue.ToDTO());
        }


        [HttpPost("assign")]

        public async Task<ActionResult<IncidentDTO>> AssignUser([FromForm] AssignIncidentData formData, CancellationToken cancellationToken)
        {
            return await ProcessNonCreateCommand(_assignIncidentCommand, formData, cancellationToken);
        }

        [HttpPost("acknowledge")]

        public async Task<ActionResult<IncidentDTO>> MoveToStandy([FromForm] AcknowledgeIncidentData formData, CancellationToken cancellationToken)
        {
            return await ProcessNonCreateCommand(_acknowledgeIncidentCommand, formData, cancellationToken);
        }

        [HttpPost("movetostandy")]

        public async Task<ActionResult<IncidentDTO>> MoveToInProgress([FromForm] MoveIncidentToStandByData formData, CancellationToken cancellationToken)
        {
            return await ProcessNonCreateCommand(_moveIncidentToStandByCommand, formData, cancellationToken);
        }


        [HttpPost("movetoinprogress")]

        public async Task<ActionResult<IncidentDTO>> MoveToStandy([FromForm] MoveIncidentToInProgressData formData, CancellationToken cancellationToken)
        {
            return await ProcessNonCreateCommand(_moveIncidentToInProgressCommand, formData, cancellationToken);
        }



        [HttpPost("validate")]

        public async Task<ActionResult<IncidentDTO>> Validate([FromForm] ValidateIncidentData formData, CancellationToken cancellationToken)
        {
            return await ProcessNonCreateCommand(_validateIncidentCommand, formData, cancellationToken);
        }

        [HttpPost("complete")]

        public async Task<ActionResult<IncidentDTO>> Complete([FromForm] CompleteIncidentData formData, CancellationToken cancellationToken)
        {
            return await ProcessNonCreateCommand(_completeIncidentCommand, formData, cancellationToken);
        }


        [HttpPost("reopen")]

        public async Task<ActionResult<IncidentDTO>> ReOpen([FromForm] ReOpenIncidentData formData, CancellationToken cancellationToken)
        {
            return await ProcessNonCreateCommand(_reOpenIncidentCommand, formData, cancellationToken);
        }

        [HttpPost("resume")]

        public async Task<ActionResult<IncidentDTO>> Resume([FromForm] ResumeIncidentData formData, CancellationToken cancellationToken)
        {
            return await ProcessNonCreateCommand(_resumeIncidentCommand, formData, cancellationToken);
        }


        [HttpGet("all")]

        public async Task<ActionResult<IncidentDTO[]>> GetAll(CancellationToken cancellationToken)
        {
            EventId eventId = new EventId(0, Guid.NewGuid().ToString());
            var incidents = await _incidentQueries.GetAllIncidents(eventId, cancellationToken);
            return incidents.Select(i => i.ToDTO()).ToArray();
        }

        private async Task<ActionResult<IncidentDTO>> ProcessNonCreateCommand<TCommand, TCommandData>(TCommand command, TCommandData commandData, CancellationToken cancellationToken) where TCommand : ICommand<TCommandData, Result<Incident>>
        {
            EventId eventId = new EventId(0, Guid.NewGuid().ToString());
            Result<Incident> result = await command.ProcessCommandAsync(commandData, eventId, cancellationToken);

            if (result.HasError)
                return BadRequest(result.DomainErrors);

            return Ok(result.EntityValue.ToDTO());
        }
    }
}
