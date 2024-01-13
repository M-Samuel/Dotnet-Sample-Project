using System;
using Dotnet.EventSourcing.SharedKernel;

namespace Dotnet.EventSourcing.Domain.UserDomain.UserErrors
{
	public record UserAlreadyExistsError(string Message) : IError;
	public record UserFirstNameEmptyError(string Message) : IError;
	public record UserLastNameEmptyError(string Message) : IError;
}

