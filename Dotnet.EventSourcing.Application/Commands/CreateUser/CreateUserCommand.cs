using Dotnet.EventSourcing.Domain.UserDomain;
using Dotnet.EventSourcing.Domain.UserDomain.UserDomainEvents;
using Dotnet.EventSourcing.SharedKernel;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dotnet.EventSourcing.Application.Commands.CreateUser
{
    public class CreateUserCommand : ICreateUserCommand
    {
        private readonly IUserService _userService;
        private readonly ILogger<CreateUserCommand> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserCommand(IUserService userService, ILogger<CreateUserCommand> logger, IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _logger = logger;
            _unitOfWork = unitOfWork;

        }
        public async Task<Result<User>> ProcessCommandAsync(CreateUserData commandData, EventId eventId, CancellationToken cancellationToken)
        {
            _logger.LogInformation(eventId, $"{nameof(CreateUserCommand)} called with parameters: {JsonSerializer.Serialize(commandData)}");

            var result = await _userService.ProcessDomainEvent(commandData.ToEvent(), cancellationToken);
            if(!result.HasError)
                await _unitOfWork.SaveAsync(cancellationToken);
            return result;
        }
    }
}
