using Dotnet.EventSourcing.Domain.IncidentDomain;
using Dotnet.EventSourcing.Domain.IncidentDomain.IncidentDomainEvents;
using Dotnet.EventSourcing.Domain.UserDomain;
using Moq;

namespace Dotnet.EventSourcing.DomainTests.IncidentDomain;

[TestFixture]
public class AcknowledgeIncidentEventTest
{

    [Test]
    public async Task AcknowledgeIncident_Correct_RetursNoError()
    {
        User user = new User()
        {
            Id = Guid.NewGuid(),
        };
        Incident incident = new Incident()
        {
            Id = Guid.NewGuid(),
            Assignee = user,
            Status = IncidentStatus.Opened
        };


        var usrMock = new Mock<IUserRepository>();
        usrMock
            .Setup(usr => usr.GetUserByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<User?>(user));

        var incMock = new Mock<IIncidentRepository>();
        incMock
            .Setup(inc => inc.GetIncidentByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<Incident?>(incident));


        IUserRepository userRepository = usrMock.Object;
        IIncidentRepository incidentRepository = incMock.Object;
        IncidentService incidentService = new (incidentRepository, userRepository);

        AcknowledgeIncidentEvent acknowledgeIncidentEvent = new(
            DateTime.UtcNow,
            incident.Id,
            user.Id
        );

        var result = await incidentService.ProcessDomainEvent(acknowledgeIncidentEvent, default);

        Assert.IsFalse(result.HasError);

    }

    [Test]
    public async Task AcknowledgeIncident_FakeIncident_RetursIncidentNotExistsError()
    {
        User user = new User()
        {
            Id = Guid.NewGuid(),
        };
        Incident? incident = null;


        var usrMock = new Mock<IUserRepository>();
        usrMock
            .Setup(usr => usr.GetUserByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<User?>(user));

        var incMock = new Mock<IIncidentRepository>();
        incMock
            .Setup(inc => inc.GetIncidentByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<Incident?>(incident));


        IUserRepository userRepository = usrMock.Object;
        IIncidentRepository incidentRepository = incMock.Object;
        IncidentService incidentService = new(incidentRepository, userRepository);

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
        User? user = null;
        Incident? incident = new Incident()
        {
            Id = Guid.NewGuid(),
            Assignee = user,
            Status = IncidentStatus.Opened
        };


        var usrMock = new Mock<IUserRepository>();
        usrMock
            .Setup(usr => usr.GetUserByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<User?>(user));

        var incMock = new Mock<IIncidentRepository>();
        incMock
            .Setup(inc => inc.GetIncidentByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<Incident?>(incident));


        IUserRepository userRepository = usrMock.Object;
        IIncidentRepository incidentRepository = incMock.Object;
        IncidentService incidentService = new(incidentRepository, userRepository);

        AcknowledgeIncidentEvent acknowledgeIncidentEvent = new(
            DateTime.UtcNow,
            Guid.NewGuid(),
            Guid.NewGuid()
        );

        var result = await incidentService.ProcessDomainEvent(acknowledgeIncidentEvent, default);

        Assert.IsTrue(result.DomainErrors.Any(error => error is UserNotFoundError));

    }

    
}
