using System;
using Dotnet.EventSourcing.Domain.UserDomain;
using Dotnet.EventSourcing.SharedKernel;

namespace Dotnet.EventSourcing.Domain.IncidentDomain
{
    public class Incident : IEntity
    {

        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public User? Customer { get; set; }
        public User? Assignee { get; set; }
        public IncidentStatus Status { get; set; }
        public IncidentDetails? IncidentDetails { get; set; }
        public List<IncidentStatusChange> IncidentStatusChanges { get; set; } = new();

        public void UpdateStatus(IncidentStatus newStatus, User changedBy)
        {
            IncidentStatusChange incidentStatusChange = new()
            {
                ChangedBy = changedBy,
                ChangedDateTime = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                NewStatus = newStatus,
                OldStatus = Status
            };

            IncidentStatusChanges.Add(incidentStatusChange);

            Status = newStatus;
        }

        public void ChangeAssignee(User assignee)
        {
            Assignee = assignee;
        }

        public static Incident CreateNew(DateTime createdDate, User customer, IncidentDetails details)
		{
            Incident incident = new()
            {
                Id = Guid.NewGuid(),
                CreatedDate = createdDate,
                Customer = customer,
                IncidentDetails = details
            };
            return incident;
		}
	}

    public record IncidentDetails(string Title, string Description);


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

