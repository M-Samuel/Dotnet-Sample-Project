using System;
namespace Dotnet.EventSourcing.SharedKernel
{
	public interface IUnitOfWork
	{
		Task CommitChangesAsync();
	}
}

