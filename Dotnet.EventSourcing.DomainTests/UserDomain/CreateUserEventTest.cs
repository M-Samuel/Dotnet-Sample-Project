
using Dotnet.EventSourcing.Domain.UserDomain;
using Dotnet.EventSourcing.Domain.UserDomain.UserDomainEvents;
using Dotnet.EventSourcing.Domain.UserDomain.UserErrors;
using Moq;
using System.Threading;

namespace Dotnet.EventSourcing.DomainTests.UserDomain;

[TestFixture]
public class CreateUserEventTest
{


    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public async Task CreateUserEvent_Correct_ReturnsNoError()
    {

        var usrMock = new Mock<IUserRepository>();
        usrMock
            .Setup(usr => usr.CreateUserAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<Task>(Task.CompletedTask));

        IUserRepository userRepository = usrMock.Object;
        var userService = new UserService(userRepository);

        var createUserEvent = new CreateUserEvent(
            DateTime.UtcNow,
            "John",
            "Doe"
        );
        var result = await userService.ProcessDomainEvent(createUserEvent, default);

        Assert.IsFalse(result.HasError);
    }

    [Test]
    public async Task CreateUserEvent_Duplicate_ReturnsUserAlreadyExistsError()
    {
        var usrMock = new Mock<IUserRepository>();
        usrMock
            .Setup(usr => usr.CreateUserAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<Task>(Task.CompletedTask));
        usrMock
            .Setup(usr => usr.GetUserByNameAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<User?>(new User()));

        IUserRepository userRepository = usrMock.Object;
        var userService = new UserService(userRepository);

        var createUserEvent = new CreateUserEvent(
            DateTime.UtcNow,
            "John",
            "Doe"
        );
        var result = await userService.ProcessDomainEvent(createUserEvent, default);

        Assert.IsTrue(result.DomainErrors.Any(error => error is UserAlreadyExistsError));
    }

    [Test]
    public async Task CreateUserEvent_NoFirstName_ReturnsUserFirstNameEmptyError()
    {
        var usrMock = new Mock<IUserRepository>();
        usrMock
            .Setup(usr => usr.CreateUserAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<Task>(Task.CompletedTask));

        IUserRepository userRepository = usrMock.Object; 
        var userService = new UserService(userRepository);

        var createUserEvent = new CreateUserEvent(
            DateTime.UtcNow,
            "",
            "Doe"
        );
        var result = await userService.ProcessDomainEvent(createUserEvent, default);

        Assert.IsTrue(result.DomainErrors.Any(error => error is UserFirstNameEmptyError));
    }

    [Test]
    public async Task CreateUserEvent_NoLastName_ReturnsUserLastNameEmptyError()
    {
        var usrMock = new Mock<IUserRepository>();
        usrMock
            .Setup(usr => usr.CreateUserAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<Task>(Task.CompletedTask));

        IUserRepository userRepository = usrMock.Object;
        var userService = new UserService(userRepository);

        var createUserEvent = new CreateUserEvent(
            DateTime.UtcNow,
            "John",
            ""
        );
        var result = await userService.ProcessDomainEvent(createUserEvent, default);

        Assert.IsTrue(result.DomainErrors.Any(error => error is UserLastNameEmptyError));
    }
}
