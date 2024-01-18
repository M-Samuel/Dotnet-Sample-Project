using Dotnet.EventSourcing.Domain.IncidentDomain.IncidentDomainEvents;
using Dotnet.EventSourcing.Domain.UserDomain.UserDomainEvents;
using Dotnet.EventSourcing.SharedKernel;

namespace Dotnet.EventSourcing.Application.Commands.CreateUser
{
    public class CreateUserData : ICommandData<CreateUserEvent>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }

        public CreateUserEvent ToEvent()
        {
            return new CreateUserEvent(DateTime.UtcNow, FirstName, LastName);
        }
    }
}