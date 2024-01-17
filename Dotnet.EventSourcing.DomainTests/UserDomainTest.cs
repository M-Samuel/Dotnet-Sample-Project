using Dotnet.EventSourcing.Domain.UserDomain;
using Dotnet.EventSourcing.Domain.UserDomain.UserDomainEvents;
using Dotnet.EventSourcing.Domain.UserDomain.UserErrors;
using Dotnet.EventSourcing.Infrastructure.Contexts;
using Dotnet.EventSourcing.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Dotnet.EventSourcing.DomainTests;

[TestFixture]
public class UserDomainTest
{
 

    [SetUp]
    public void Setup()
    {
        
    }

    [Test]
    public async Task CreateUser_ReturnsNoError()
    {
        IUserRepository userRepository = new CreateUser_ReturnsNoError_FakeUserRepository();
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
    public async Task CreateUser_ReturnsUserAlreadyExistsError()
    {
        IUserRepository userRepository = new CreateUser_ReturnsUserAlreadyExistsError_FakeUserRepository();
        var userService = new UserService(userRepository);

        var createUserEvent = new CreateUserEvent(
            DateTime.UtcNow,
            "John",
            "Doe"
        );
        var result = await userService.ProcessDomainEvent(createUserEvent);

        Assert.IsTrue(result.DomainErrors.Any(error => error is UserAlreadyExistsError));
    }

}

class CreateUser_ReturnsNoError_FakeUserRepository : IUserRepository
{
    public async Task CreateUserAsync(User user)
    {
        await Task.CompletedTask;
    }

    public Task<User?> GetUserByIdAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetUserByNameAsync(string firstName, string lastName)
    {
        return await Task.FromResult<User?>(null);
    }
}

class CreateUser_ReturnsUserAlreadyExistsError_FakeUserRepository : IUserRepository
{
    public async Task CreateUserAsync(User user)
    {
        await Task.CompletedTask;
    }

    public Task<User?> GetUserByIdAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetUserByNameAsync(string firstName, string lastName)
    {
        User user = User.Create(firstName,lastName);
        return await Task.FromResult<User?>(user);
    }
}