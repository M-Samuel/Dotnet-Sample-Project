using System;
namespace Dotnet.EventSourcing.SharedKernel
{
	public interface IError
	{
		string Message { get; }
		string ErrorName { get; }
	}
}

