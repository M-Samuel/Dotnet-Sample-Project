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

        var result = await incidentService.ProcessDomainEvent(acknowledgeIncidentEvent, default);

        Assert.IsFalse(result.HasError);

    }

    [Test]
    public async Task AcknowledgeIncident_FakeIncident_RetursIncidentNotExistsError()
    {
        IUserRepository userRepository = new UserExists_FakeUserRepository();
        IIncidentRepository incidentRepository = new FakeIncident_RetursIncidentNotExistsError_FakeIncidentRepository();
        IncidentService incidentService = new (incidentRepository, userRepository);

        AcknowledgeIncidentEvent acknowledgeIncidentEvent = new(
            DateTime.UtcNow,
            Guid.NewGuid(),
            Guid.NewGuid()
        );

        var result = await incidentService.ProcessDomainEvent(acknowledgeIncidentEvent, default);

        Assert.IsTrue(result.DomainErrors.Any(error => error is IncidentNotFoundError));

    }

    [Test]
    public async Task AcknowledgeIncident_FakeUser_RetursUserNotFoundError()
    {
        IUserRepository userRepository = new UserNotExist_FakeUserRepository();
        IIncidentRepository incidentRepository = new Correct_RetursNoError_FakeIncidentRepository();
        IncidentService incidentService = new (incidentRepository, userRepository);

        AcknowledgeIncidentEvent acknowledgeIncidentEvent = new(
            DateTime.UtcNow,
            Guid.NewGuid(),
            Guid.NewGuid()
        );

        var result = await incidentService.ProcessDomainEvent(acknowledgeIncidentEvent, default);

        Assert.IsTrue(result.DomainErrors.Any(error => error is UserNotFoundError));

    }

    private class UserNotExist_FakeUserRepository : IUserRepository
    {
        public Task CreateUserAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await Task.FromResult<User?>(null);
        }

        public Task<User?> GetUserByNameAsync(string firstName, string lastName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    private class UserExists_FakeUserRepository : IUserRepository
    {
        public Task CreateUserAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            var user = User.Create("John", "Doe");
            user.Id = userId;
            return await Task.FromResult(user);
        }

        public Task<User?> GetUserByNameAsync(string firstName, string lastName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    private class Correct_RetursNoError_FakeIncidentRepository : IIncidentRepository
    {
        public Task CreateIncidentAsync(Incident incident, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Incident[]> GetAllIncidents(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Incident?> GetIncidentByIdAsync(Guid incidentId, CancellationToken cancellationToken)
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

        public void UpdateIncident(Incident incident)
        {
            
        }
    }

    private class FakeIncident_RetursIncidentNotExistsError_FakeIncidentRepository : IIncidentRepository
    {
        public Task CreateIncidentAsync(Incident incident, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Incident[]> GetAllIncidents(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Incident?> GetIncidentByIdAsync(Guid incidentId, CancellationToken cancellationToken)
        {
            return await Task.FromResult<Incident?>(null);
        }

        public void UpdateIncident(Incident incident)
        {
            throw new NotImplementedException();
        }
    }
}
