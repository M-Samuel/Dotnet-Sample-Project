using Dotnet.EventSourcing.Domain.UserDomain;
using Dotnet.EventSourcing.SharedKernel;

namespace Dotnet.EventSourcing.Application.Commands.CreateUser
{
    public interface ICreateUserCommand : ICommand<CreateUserData, Result<User>>
    {
    }
}