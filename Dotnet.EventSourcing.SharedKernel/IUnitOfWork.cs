using System;
namespace Dotnet.EventSourcing.SharedKernel
{
	public interface IUnitOfWork : IDisposable
	{
		Task<int> SaveChangesAsync();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

