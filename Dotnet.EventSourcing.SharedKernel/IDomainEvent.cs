using System;
namespace Dotnet.EventSourcing.SharedKernel
{
	public interface IDomainEvent
	{
		DateTime OccurranceDateTime { get; }
	}
}

