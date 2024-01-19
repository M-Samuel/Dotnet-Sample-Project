using System;
namespace Dotnet.EventSourcing.SharedKernel
{
	public interface IUnitOfWork : IDisposable
	{
		Task<int> TrySaveChangesAsync();

        Task<int> TrySaveChangesAsync(CancellationToken cancellationToken);
    }
}

