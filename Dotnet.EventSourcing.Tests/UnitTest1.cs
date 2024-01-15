using Dotnet.EventSourcing.Domain.IncidentDomain;
using Dotnet.EventSourcing.Domain.IncidentDomain.IncidentDomainEvents;
using Dotnet.EventSourcing.Domain.UserDomain;
using Dotnet.EventSourcing.Domain.UserDomain.UserDomainEvents;
using Dotnet.EventSourcing.Infrastructure.Contexts;
using Dotnet.EventSourcing.Infrastructure.Repositories;
using Dotnet.EventSourcing.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Reflection.Metadata;

namespace Dotnet.EventSourcing.Tests;

[TestClass]
public class InMemoryDBTest
{
    private readonly DbContextOptions<DatabaseContext> _contextOptions;
    private readonly User[] _users = new User[]{
        new User() { FirstName = "Sam", LastName = "Modeste", Id = Guid.NewGuid() },
        new User() { FirstName = "Jo", LastName = "Modeste", Id = Guid.NewGuid() }
    };

    public InMemoryDBTest()
    {
        _contextOptions = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase("IncidentTest")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;


        

        using (var context = new DatabaseContext(_contextOptions))
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.AddRange(_users);
            context.SaveChanges();
        }

        
    }

    [TestMethod]
    public async Task QueryByName()
    {
        using var context = new DatabaseContext(_contextOptions);
        UserRepository userRepository = new UserRepository(context);

        Domain.UserDomain.User? user = await userRepository.GetUserByNameAsync("Sam", "Modeste");
        Assert.IsNotNull(user);

    }

    [TestMethod]
    public async Task CreateUser()
    {
        using var context = new DatabaseContext(_contextOptions);
        UserRepository userRepository = new UserRepository(context);

        await userRepository.CreateUserAsync(new Domain.UserDomain.User() { FirstName = "Kevin", LastName = "Modeste", Id = Guid.NewGuid() });

        context.SaveChanges();


        Domain.UserDomain.User? user = await userRepository.GetUserByNameAsync("Kevin", "Modeste");

        Assert.IsNotNull(user);

    }

    [TestMethod]
    public async Task UserServiceCreateUser()
    {
        using var context = new DatabaseContext(_contextOptions);
        UserRepository userRepository = new UserRepository(context);
        UserService userService = new(userRepository);

        var result = await userService.ProcessDomainEvent(new CreateUserEvent(DateTime.UtcNow, "Sam", "Modeste"));

        foreach (IError error in result.DomianErrors)
        {
            Console.WriteLine(error.Message);
        }
        Assert.IsTrue(result.HasError);


    }

    [TestMethod]
    public async Task CreateIncident()
    {
        using (var context = new DatabaseContext(_contextOptions))
        {
            var incidentService = new IncidentService(new IncidentRepository(context), new UserRepository(context));

            OpenIncidentEvent openIncidentEvent = new(
                DateTime.UtcNow,
                _users[0].Id,
                new Domain.IncidentDomain.IncidentDetails("Database not responding", "Slow response time on database level")
            );

            var result = await incidentService.ProcessDomainEvent(openIncidentEvent);

            Assert.IsFalse(result.HasError);
        }
    }



}
