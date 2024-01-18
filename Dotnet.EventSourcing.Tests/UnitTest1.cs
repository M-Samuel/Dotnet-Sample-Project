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


        //_contextOptions = new DbContextOptionsBuilder<DatabaseContext>()
        //    .UseNpgsql("Host=127.0.0.1;Port=5433;Database=Dotnet_Test;Username=APP;Password=password")
        //    .EnableSensitiveDataLogging()
        //    .Options;



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

        foreach (IError error in result.DomainErrors)
        {
            Console.WriteLine(error.Message);
        }
        Assert.IsTrue(result.HasError);


    }

    [TestMethod]
    public async Task CreateIncident()
    {

        Guid incidentID;
        using (var context = new DatabaseContext(_contextOptions))
        {
            var incidentService = new IncidentService(new IncidentRepository(context), new UserRepository(context));

            OpenIncidentEvent openIncidentEvent = new(
                DateTime.UtcNow,
                _users[0].Id,
                "Database not responding", 
                "Slow response time on database level"
            );

            var result = await incidentService.ProcessDomainEvent(openIncidentEvent);
            incidentID = result.EntityValue.Id;

            
            await context.SaveChangesAsync();


            AssignIncidentEvent assignIncidentEvent = new(
                DateTime.UtcNow,
                incidentID,
                _users[1].Id
            );

            var result2 = await incidentService.ProcessDomainEvent(assignIncidentEvent);

            await context.SaveChangesAsync();


            Assert.IsTrue(!result.HasError);

        }
    }



}
