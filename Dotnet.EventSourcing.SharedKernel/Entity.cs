using System;
namespace Dotnet.EventSourcing.SharedKernel
{
	public class Entity
	{
		public Entity(Guid id)
		{
			Id = id;
		}

        public Guid Id { get; }
    }
}

