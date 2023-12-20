using System;
namespace Dotnet.EventSourcing.SharedKernel
{
	public interface IEntity
    { 
        public Guid Id { get; }
    }
}

