using System;
using Dotnet.EventSourcing.SharedKernel;

namespace Dotnet.EventSourcing.Domain.UserDomain.UserErrors
{
	public record UserAlreadyExistsError(string Message, string ErrorName = nameof(UserAlreadyExistsError)) : IError;
	public record UserFirstNameEmptyError(string Message, string ErrorName = nameof(UserFirstNameEmptyError)) : IError;
	public record UserLastNameEmptyError(string Message, string ErrorName = nameof(UserLastNameEmptyError)) : IError;
}

