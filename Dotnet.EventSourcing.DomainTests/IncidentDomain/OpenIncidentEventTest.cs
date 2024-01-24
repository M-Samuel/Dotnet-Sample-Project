using Dotnet.EventSourcing.Domain.IncidentDomain;
using Dotnet.EventSourcing.Domain.IncidentDomain.IncidentDomainEvents;
using Dotnet.EventSourcing.Domain.UserDomain;
using Moq;

namespace Dotnet.EventSourcing.DomainTests.IncidentDomain;

[TestFixture]
public class OpenIncidentEventTest
{
    [Test]
    public async Task OpenIncident_Correct_RetursNoError()
    {
        User user = new User()
        {
            Id = Guid.NewGuid(),
        };


        var usrMock = new Mock<IUserRepository>();
        usrMock
            .Setup(usr => usr.GetUserByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<User?>(user));

        var incMock = new Mock<IIncidentRepository>();
        incMock
            .Setup(inc => inc.CreateIncidentAsync(It.IsAny<Incident>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<Task>(Task.CompletedTask));


        IUserRepository userRepository = usrMock.Object;
        IIncidentRepository incidentRepository = incMock.Object;
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
        User? user = null;


        var usrMock = new Mock<IUserRepository>();
        usrMock
            .Setup(usr => usr.GetUserByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<User?>(user));

        var incMock = new Mock<IIncidentRepository>();
        incMock
            .Setup(inc => inc.CreateIncidentAsync(It.IsAny<Incident>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);


        IUserRepository userRepository = usrMock.Object;
        IIncidentRepository incidentRepository = incMock.Object;
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
}


