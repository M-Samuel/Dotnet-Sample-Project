using System;
namespace Dotnet.EventSourcing.SharedKernel
{
	public interface IUnitOfWork : IDisposable
	{
		Task SaveChangesAsync();
	}
}

