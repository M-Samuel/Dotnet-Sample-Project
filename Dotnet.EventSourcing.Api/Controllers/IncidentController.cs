using Dotnet.EventSourcing.Application.Commands.AcknowledgeIncident;
using Dotnet.EventSourcing.Application.Commands.AssignIncident;
using Dotnet.EventSourcing.Application.Commands.MoveIncidentToInProgress;
using Dotnet.EventSourcing.Application.Commands.MoveIncidentToStandBy;
using Dotnet.EventSourcing.Application.Commands.OpenIncident;
using Dotnet.EventSourcing.Application.DTOS;
using Dotnet.EventSourcing.Application.Queries;
using Dotnet.EventSourcing.Domain.IncidentDomain;
using Dotnet.EventSourcing.SharedKernel;
using Microsoft.AspNetCore.Mvc;

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

        public IncidentController(IIncidentQueries incidentQueries
            ,IOpenIncidentCommand openIncidentCommand
            ,IAssignIncidentCommand assignIncidentCommand
            ,IMoveIncidentToInProgressCommand moveIncidentToInProgressCommand
            ,IMoveIncidentToStandByCommand moveIncidentToStandByCommand
            ,IAcknowledgeIncidentCommand acknowledgeIncidentCommand)
        {
            _incidentQueries = incidentQueries;
            _openIncidentCommand = openIncidentCommand;
            _assignIncidentCommand = assignIncidentCommand;
            _moveIncidentToInProgressCommand = moveIncidentToInProgressCommand;
            _moveIncidentToStandByCommand = moveIncidentToStandByCommand;
            _acknowledgeIncidentCommand = acknowledgeIncidentCommand;
        }


        [HttpGet("/incident/get/{id}")]
        public async Task<ActionResult<IncidentDTO>> GetIncidentById(Guid id, CancellationToken cancellationToken)
        {
            EventId eventId = new EventId(0, Guid.NewGuid().ToString());
            var incident = await _incidentQueries.GetIncidentByIdAsync(id, eventId, cancellationToken);
            if (incident == null)
                return NotFound();
            return Ok(incident.ToDTO());
        }

        [HttpPost("/incident/open")]

        public async Task<ActionResult<IncidentDTO>> CreateUser([FromForm] OpenIncidentData formData, CancellationToken cancellationToken)
        {
            EventId eventId = new EventId(0, Guid.NewGuid().ToString());
            Result<Incident> result = await _openIncidentCommand.ProcessCommandAsync(formData, eventId, cancellationToken);

            if (result.HasError)
                return BadRequest(result.DomainErrors);

            return new CreatedAtActionResult(nameof(GetIncidentById), "Incident", new { id = result.EntityValue.Id }, result.EntityValue.ToDTO());
        }


        [HttpPost("/incident/assign")]

        public async Task<ActionResult<IncidentDTO>> AssignUser([FromForm] AssignIncidentData formData, CancellationToken cancellationToken)
        {
            EventId eventId = new EventId(0, Guid.NewGuid().ToString());
            Result<Incident> result = await _assignIncidentCommand.ProcessCommandAsync(formData, eventId, cancellationToken);

            if (result.HasError)
                return BadRequest(result.DomainErrors);

            return Ok(result.EntityValue.ToDTO());
        }

        [HttpPost("/incident/acknowledge")]

        public async Task<ActionResult<IncidentDTO>> MoveToStandy([FromForm] AcknowledgeIncidentData formData, CancellationToken cancellationToken)
        {
            EventId eventId = new EventId(0, Guid.NewGuid().ToString());
            Result<Incident> result = await _acknowledgeIncidentCommand.ProcessCommandAsync(formData, eventId, cancellationToken);

            if (result.HasError)
                return BadRequest(result.DomainErrors);

            return Ok(result.EntityValue.ToDTO());
        }

        [HttpPost("/incident/movetostandy")]

        public async Task<ActionResult<IncidentDTO>> MoveToInProgress([FromForm] MoveIncidentToStandByData formData, CancellationToken cancellationToken)
        {
            EventId eventId = new EventId(0, Guid.NewGuid().ToString());
            Result<Incident> result = await _moveIncidentToStandByCommand.ProcessCommandAsync(formData, eventId, cancellationToken);

            if (result.HasError)
                return BadRequest(result.DomainErrors);

            return Ok(result.EntityValue.ToDTO());
        }


        [HttpPost("/incident/movetoinprogress")]

        public async Task<ActionResult<IncidentDTO>> MoveToStandy([FromForm] MoveIncidentToInProgressData formData, CancellationToken cancellationToken)
        {
            EventId eventId = new EventId(0, Guid.NewGuid().ToString());
            Result<Incident> result = await _moveIncidentToInProgressCommand.ProcessCommandAsync(formData, eventId, cancellationToken);

            if (result.HasError)
                return BadRequest(result.DomainErrors);

            return Ok(result.EntityValue.ToDTO());
        }
    }
}
