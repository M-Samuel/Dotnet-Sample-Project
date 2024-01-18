using Dotnet.EventSourcing.Application.Commands.CreateUser;
using Dotnet.EventSourcing.Application.Commands.OpenIncident;
using Dotnet.EventSourcing.Application.Queries;
using Dotnet.EventSourcing.Domain.IncidentDomain;
using Dotnet.EventSourcing.Domain.UserDomain;
using Dotnet.EventSourcing.Infrastructure.Contexts;
using Dotnet.EventSourcing.Infrastructure.Repositories;
using Dotnet.EventSourcing.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DatabaseContext>(
    options => options
            .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole().AddDebug()))
            .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test")
            //.UseInMemoryDatabase("IncidentTest")
            .EnableDetailedErrors()
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
);
#pragma warning disable CS8603 // Possible null reference return.
builder.Services.AddScoped<IUnitOfWork>(s => s.GetService<DatabaseContext>());
#pragma warning restore CS8603 // Possible null reference return.

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IIncidentRepository, IncidentRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IIncidentService, IncidentService>();

builder.Services.AddScoped<IUserQueries,  UserQueries>();
builder.Services.AddScoped<ICreateUserCommand, CreateUserCommand>();

builder.Services.AddScoped<IIncidentQueries, IncidentQueries>();
builder.Services.AddScoped<IOpenIncidentCommand, OpenIncidentCommand>();

var app = builder.Build();

var serviceScopeFactory = app.Services.GetService<IServiceScopeFactory>();
if(serviceScopeFactory != null)
{
    using (var scope = serviceScopeFactory.CreateScope())
    using (var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>())
    {
        context.Database.EnsureCreated();
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

