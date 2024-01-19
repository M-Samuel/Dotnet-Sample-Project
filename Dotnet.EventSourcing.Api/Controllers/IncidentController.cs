using Dotnet.EventSourcing.Application.Commands.AssignIncident;
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

        public IncidentController(IIncidentQueries incidentQueries
            ,IOpenIncidentCommand openIncidentCommand
            ,IAssignIncidentCommand assignIncidentCommand)
        {
            _incidentQueries = incidentQueries;
            _openIncidentCommand = openIncidentCommand;
            _assignIncidentCommand = assignIncidentCommand;
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

        public async Task<ActionResult<IncidentDTO>> CreateUser([FromForm] OpenIncidentData openIncidentData, CancellationToken cancellationToken)
        {
            EventId eventId = new EventId(0, Guid.NewGuid().ToString());
            Result<Incident> result = await _openIncidentCommand.ProcessCommandAsync(openIncidentData, eventId, cancellationToken);

            if (result.HasError)
                return BadRequest(result.DomainErrors);

            return new CreatedAtActionResult(nameof(GetIncidentById), "Incident", new { id = result.EntityValue.Id }, result.EntityValue.ToDTO());
        }


        [HttpPost("/incident/assign")]

        public async Task<ActionResult<IncidentDTO>> AssignUser([FromForm] AssignIncidentData assignIncidentData, CancellationToken cancellationToken)
        {
            EventId eventId = new EventId(0, Guid.NewGuid().ToString());
            Result<Incident> result = await _assignIncidentCommand.ProcessCommandAsync(assignIncidentData, eventId, cancellationToken);

            if (result.HasError)
                return BadRequest(result.DomainErrors);

            return Ok(result.EntityValue.ToDTO());
        }
    }
}
