using Dotnet.EventSourcing.Domain.UserDomain;
using Dotnet.EventSourcing.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.EventSourcing.Application.Commands.CreateUser
{
    public interface ICreateUserCommand : ICommand<CreateUserData, Result<User>>
    {
    }
}
