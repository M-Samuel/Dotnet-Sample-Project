using Dotnet.EventSourcing.Domain.IncidentDomain;
using Dotnet.EventSourcing.Domain.IncidentDomain.IncidentDomainEvents;
using Dotnet.EventSourcing.Domain.UserDomain;

namespace Dotnet.EventSourcing.DomainTests.IncidentDomain;

[TestFixture]
public class AcknowledgeIncidentEventTest
{
    [Test]
    public async Task AcknowledgeIncident_Correct_RetursNoError()
    {
        IUserRepository userRepository = new UserExists_FakeUserRepository();
        IIncidentRepository incidentRepository = new Correct_RetursNoError_FakeIncidentRepository();
        IncidentService incidentService = new (incidentRepository, userRepository);

        AcknowledgeIncidentEvent acknowledgeIncidentEvent = new(
            DateTime.UtcNow,
            Guid.NewGuid(),
            Guid.NewGuid()
        );

        var result = await incidentService.ProcessDomainEvent(acknowledgeIncidentEvent);

        Assert.IsFalse(result.HasError);

    }

    private class UserExists_FakeUserRepository : IUserRepository
    {
        public Task CreateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            var user = User.Create("John", "Doe");
            user.Id = userId;
            return await Task.FromResult(user);
        }

        public Task<User?> GetUserByNameAsync(string firstName, string lastName)
        {
            throw new NotImplementedException();
        }
    }

    private class Correct_RetursNoError_FakeIncidentRepository : IIncidentRepository
    {
        public Task CreateIncidentAsync(Incident incident)
        {
            throw new NotImplementedException();
        }

        public async Task<Incident?> GetIncidentByIdAsync(Guid incidentId)
        {
            Incident incident = Incident.CreateNew(
                DateTime.UtcNow,
                User.Create("John","Doe"),
                "Slow Database",
                "Desc..."
            );
            incident.Id = incidentId;
            incident.Status = IncidentStatus.Opened;
            incident.Assignee = incident.Customer;

            return await Task.FromResult(incident);
        }

        public async Task UpdateIncidentAsync(Incident incident)
        {
            await Task.CompletedTask;
        }
    }
}
