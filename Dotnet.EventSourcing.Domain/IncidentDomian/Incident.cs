using System;
using Dotnet.EventSourcing.Domain.UserDomain;
using Dotnet.EventSourcing.SharedKernel;

namespace Dotnet.EventSourcing.Domain.IncidentDomain
{
    public partial class Incident : IEntity
    {

        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public User? Customer { get; set; }
        public User? Assignee { get; set; }
        public IncidentStatus Status { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<IncidentStatusChange> IncidentStatusChanges { get; set; } = new();

        public void UpdateStatus(IncidentStatus newStatus, User changedBy)
        {
            IncidentStatusChange incidentStatusChange = new()
            {
                ChangedBy = changedBy,
                ChangedDateTime = DateTime.UtcNow,
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

        public static Incident CreateNew(DateTime createdDate, User customer, string title, string description)
		{
            Incident incident = new()
            {
                CreatedDate = createdDate,
                Customer = customer,
                Title = title,
                Description = description
            };
            return incident;
		}
	}



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

