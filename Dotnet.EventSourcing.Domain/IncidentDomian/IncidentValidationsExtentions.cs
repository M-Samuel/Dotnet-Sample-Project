﻿using System;
using Dotnet.EventSourcing.Domain.IncidentDomain;

namespace Dotnet.EventSourcing.Domain.IncidentDomian
{
	public static class IncidentValidationsExtentions
    {
		public static bool ValidateCanAcknowledge(this Incident incident)
		{
			return incident.Status == IncidentStatus.Opened;
		}

        public static bool ValidateCanInProgress(this Incident incident)
        {
            return
                incident.Status == IncidentStatus.Acknowledged ||
                incident.Status == IncidentStatus.InProgress;
        }

        public static bool ValidateCanStandy(this Incident incident)
        {
            return incident.Status == IncidentStatus.InProgress;
        }

        public static bool ValidateCanComplete(this Incident incident)
        {
            return incident.Status == IncidentStatus.InProgress;
        }

        public static bool ValidateCanClose(this Incident incident)
        {
            return incident.Status == IncidentStatus.Completed;
        }

        public static bool ValidateCanReOpen(this Incident incident)
        {
            return incident.Status == IncidentStatus.Completed;
        }
    }
}

