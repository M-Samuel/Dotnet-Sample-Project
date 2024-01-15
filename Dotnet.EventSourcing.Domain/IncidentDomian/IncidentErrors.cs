using System;
using Dotnet.EventSourcing.Domain.IncidentDomain;
using Dotnet.EventSourcing.SharedKernel;

namespace Dotnet.EventSourcing.Domain.IncidentDomain
{
	public static class IncidentErrors
	{
		public static CannotAcknowledgeIncidentNotOpenedError CannotAcknowledgeIncidentNotOpenedError =>
			new("Cannot acknowledge Incident as status is not opened");
        public static CannotInProgressIncidentNotAcknowledgeError CannotInProgressIncidentNotAcknowledgeError =>
            new("Cannot change Incident to In Progress as status is not Acknowledge");
        public static CannotInProgressIncidentNotStandByError CannotInProgressIncidentNotStandByError =>
            new("Cannot change Incident to In Progress as status is not Standby");
        public static CannotStandByIncidentNotInProgressError CannotStandByIncidentNotInProgressError =>
            new("Cannot change Incident to StandBy as status is not In Progress");
        public static CannotCompleteIncidentNotInProgressError CannotCompleteIncidentNotInProgressError =>
            new("Cannot change Incident to Completed as status is not In Progress");
        public static CannotCloseIncidentNotCompletedError CannotCloseIncidentNotCompletedError =>
            new("Cannot change Incident to Closed as status is not Completed");
        public static CannotReOpenIncidentNotCompletedError CannotReOpenIncidentNotCompletedError =>
            new("Cannot change Incident to Closed as status is not Completed");
        public static CannotAcknowledgeAsNoAssigneeError CannotAcknowledgeAsNoAssigneeError =>
            new("Cannot acknowledge Incident as incident has no assignee");
    }

	public record CannotAcknowledgeIncidentNotOpenedError(string Message, string ErrorName = nameof(CannotAcknowledgeIncidentNotOpenedError)) : IError;
    public record CannotInProgressIncidentNotAcknowledgeError(string Message, string ErrorName = nameof(CannotInProgressIncidentNotAcknowledgeError)) : IError;
    public record CannotInProgressIncidentNotStandByError(string Message, string ErrorName = nameof(CannotInProgressIncidentNotStandByError)) : IError;
    public record CannotStandByIncidentNotInProgressError(string Message, string ErrorName = nameof(CannotStandByIncidentNotInProgressError)) : IError;
    public record CannotCompleteIncidentNotInProgressError(string Message, string ErrorName = nameof(CannotCompleteIncidentNotInProgressError)) : IError;
    public record CannotCloseIncidentNotCompletedError(string Message, string ErrorName = nameof(CannotCloseIncidentNotCompletedError)) : IError;
    public record CannotReOpenIncidentNotCompletedError(string Message, string ErrorName = nameof(CannotReOpenIncidentNotCompletedError)) : IError;
    public record CannotAcknowledgeAsNoAssigneeError(string Message, string ErrorName = nameof(CannotAcknowledgeAsNoAssigneeError)) : IError;

    public record UserNotFoundError(string Message, string ErrorName = nameof(UserNotFoundError)) : IError;
    public record IncidentNotFoundError(string Message, string ErrorName = nameof(IncidentNotFoundError)) : IError;
}

