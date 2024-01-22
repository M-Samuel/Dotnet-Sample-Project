using Dotnet.EventSourcing.Domain.IncidentDomain;
using Dotnet.EventSourcing.Domain.IncidentDomain.IncidentDomainEvents;
using Dotnet.EventSourcing.Domain.UserDomain;

namespace Dotnet.EventSourcing.DomainTests.IncidentDomain;

[TestFixture]
public class OpenIncidentEventTest
{
    [Test]
    public async Task OpenIncident_Correct_RetursNoError()
    {
        IUserRepository userRepository = new UserExists_FakeUserRepository();
        IIncidentRepository incidentRepository = new Correct_RetursNoError_FakeIncidentRepository();
        IncidentService incidentService = new(incidentRepository, userRepository);

        OpenIncidentEvent openIncidentEvent = new(
            DateTime.UtcNow,
            Guid.NewGuid(),
            "Slow Database Response",
            "Connection Pool saturatued"
        );

        var result = await incidentService.ProcessDomainEvent(openIncidentEvent, default);

        Assert.IsFalse(result.HasError);

    }

    [Test]
    public async Task OpenIncident_FakeUser_RetursUserNotFoundError()
    {
        IUserRepository userRepository = new UserDoesNotExists_FakeUserRepository();
        IIncidentRepository incidentRepository = new FakeUser_RetursUserNotFoundError_FakeIncidentRepository();
        IncidentService incidentService = new(incidentRepository, userRepository);

        OpenIncidentEvent openIncidentEvent = new(
            DateTime.UtcNow,
            Guid.NewGuid(),
            "Slow Database Response",
            "Connection Pool saturatued"
        );

        var result = await incidentService.ProcessDomainEvent(openIncidentEvent, default);

        Assert.IsTrue(result.DomainErrors.Any(error => error is UserNotFoundError));

    }


    private class FakeUser_RetursUserNotFoundError_FakeIncidentRepository : IIncidentRepository
    {
        public async Task CreateIncidentAsync(Incident incident, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }

        public Task<Incident[]> GetAllIncidents(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Incident?> GetIncidentByIdAsync(Guid incidentId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void UpdateIncident(Incident incident)
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

    private class UserDoesNotExists_FakeUserRepository : IUserRepository
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

    private class Correct_RetursNoError_FakeIncidentRepository : IIncidentRepository
    {
        public async Task CreateIncidentAsync(Incident incident, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }

        public Task<Incident[]> GetAllIncidents(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Incident?> GetIncidentByIdAsync(Guid incidentId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void UpdateIncident(Incident incident)
        {
            throw new NotImplementedException();
        }
    }
}


