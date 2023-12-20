using System;
using Dotnet.EventSourcing.Domain.CustomerDomain;
using Dotnet.EventSourcing.Domain.IncidentDomian;
using Dotnet.EventSourcing.Domain.IncidentDomian.IncidentDomainEvents;
using Dotnet.EventSourcing.SharedKernel;

namespace Dotnet.EventSourcing.Domain.IncidentDomain
{
	public class Incident : Entity
	{
		public Incident(Guid id, DateTime createdDate, Customer customer, IncidentDetails details) : base(id)
		{
			CreatedDate = createdDate;
			Customer = customer;
            IncidentDetails = details;
            _incidentStatusChanges = new();
        }

        public Incident(Guid id, DateTime createdDate, Customer customer, IncidentDetails details, List<IncidentStatusChange> incidentStatusChanges) : base(id)
        {
            CreatedDate = createdDate;
            Customer = customer;
            IncidentDetails = details;
            _incidentStatusChanges = incidentStatusChanges;
        }

        public DateTime CreatedDate { get; set; }
		public Customer Customer { get; set; }
        public IncidentStatus Status { get; set; } = IncidentStatus.None;
        public IncidentDetails IncidentDetails { get; set; }

        private readonly List<IncidentStatusChange> _incidentStatusChanges;
        public List<IncidentStatusChange> IncidentStatusChanges => _incidentStatusChanges.ToList();

        public void UpdateStatus(IncidentStatus newStatus)
        {
            _incidentStatusChanges.Add(new IncidentStatusChange(Status, newStatus, DateTime.UtcNow));
            Status = newStatus;
        }

        public static Incident CreateNew(DateTime createdDate, Customer customer, IncidentDetails details)
		{
            Incident incident = new(Guid.NewGuid(), createdDate, customer, details);
            incident.UpdateStatus(IncidentStatus.Opened);
            return incident;
		}
	}

    public record IncidentDetails(string Title, string Description);

    public record IncidentStatusChange(
        IncidentStatus OldStatus,
        IncidentStatus NewStatus,
        DateTime ChangedDateTime
    );


    public enum IncidentStatus
    {
        None,
        Opened,
        Acknowledged,
        InProgress,
        StandBy,
        Completed,
        Closed
    }

}

