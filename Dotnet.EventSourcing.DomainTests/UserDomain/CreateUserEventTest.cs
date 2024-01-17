
using Dotnet.EventSourcing.Domain.UserDomain;
using Dotnet.EventSourcing.Domain.UserDomain.UserDomainEvents;
using Dotnet.EventSourcing.Domain.UserDomain.UserErrors;

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
        IUserRepository userRepository = new Correct_ReturnsNoError_FakeUserRepository();
        var userService = new UserService(userRepository);

        var createUserEvent = new CreateUserEvent(
            DateTime.UtcNow,
            "John",
            "Doe"
        );
        var result = await userService.ProcessDomainEvent(createUserEvent);

        Assert.IsFalse(result.HasError);
    }

    [Test]
    public async Task CreateUserEvent_Duplicate_ReturnsUserAlreadyExistsError()
    {
        IUserRepository userRepository = new Duplicate_ReturnsUserAlreadyExistsError_FakeUserRepository();
        var userService = new UserService(userRepository);

        var createUserEvent = new CreateUserEvent(
            DateTime.UtcNow,
            "John",
            "Doe"
        );
        var result = await userService.ProcessDomainEvent(createUserEvent);

        Assert.IsTrue(result.DomainErrors.Any(error => error is UserAlreadyExistsError));
    }

    [Test]
    public async Task CreateUserEvent_NoFirstName_ReturnsUserFirstNameEmptyError()
    {
        IUserRepository userRepository = new NoFirstName_ReturnsUserFirstNameEmptyError_FakeUserRepository();
        var userService = new UserService(userRepository);

        var createUserEvent = new CreateUserEvent(
            DateTime.UtcNow,
            "",
            "Doe"
        );
        var result = await userService.ProcessDomainEvent(createUserEvent);

        Assert.IsTrue(result.DomainErrors.Any(error => error is UserFirstNameEmptyError));
    }

    [Test]
    public async Task CreateUserEvent_NoLastName_ReturnsUserLastNameEmptyError()
    {
        IUserRepository userRepository = new NoLastName_ReturnsUserLastNameEmptyError_FakeUserRepository();
        var userService = new UserService(userRepository);

        var createUserEvent = new CreateUserEvent(
            DateTime.UtcNow,
            "John",
            ""
        );
        var result = await userService.ProcessDomainEvent(createUserEvent);

        Assert.IsTrue(result.DomainErrors.Any(error => error is UserLastNameEmptyError));
    }


    private class Correct_ReturnsNoError_FakeUserRepository : IUserRepository
    {
        public async Task CreateUserAsync(User user)
            => await Task.CompletedTask;

        public Task<User?> GetUserByIdAsync(Guid userId)
            => throw new NotImplementedException();

        public async Task<User?> GetUserByNameAsync(string firstName, string lastName)
            => await Task.FromResult<User?>(null);
    }

    private class Duplicate_ReturnsUserAlreadyExistsError_FakeUserRepository : IUserRepository
    {
        public async Task CreateUserAsync(User user)
            => await Task.CompletedTask;

        public Task<User?> GetUserByIdAsync(Guid userId)
            => throw new NotImplementedException();

        public async Task<User?> GetUserByNameAsync(string firstName, string lastName)
        {
            User user = User.Create(firstName, lastName);
            return await Task.FromResult<User?>(user);
        }
    }

    private class NoFirstName_ReturnsUserFirstNameEmptyError_FakeUserRepository : IUserRepository
    {
        public async Task CreateUserAsync(User user)
            => await Task.CompletedTask;

        public Task<User?> GetUserByIdAsync(Guid userId)
            => throw new NotImplementedException();

        public async Task<User?> GetUserByNameAsync(string firstName, string lastName)
            => await Task.FromResult<User?>(null);
    }

    private class NoLastName_ReturnsUserLastNameEmptyError_FakeUserRepository : IUserRepository
    {
        public async Task CreateUserAsync(User user)
            => await Task.CompletedTask;

        public Task<User?> GetUserByIdAsync(Guid userId)
            => throw new NotImplementedException();

        public async Task<User?> GetUserByNameAsync(string firstName, string lastName)
            => await Task.FromResult<User?>(null);
    }

}
