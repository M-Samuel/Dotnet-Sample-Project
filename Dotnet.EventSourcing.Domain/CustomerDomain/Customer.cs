using System;
using Dotnet.EventSourcing.SharedKernel;

namespace Dotnet.EventSourcing.Domain.CustomerDomain
{
	public class Customer : Entity
	{
		private Customer(Guid id, FullName fullName) : base(id)
		{
			FullName = fullName;
		}

        public FullName FullName { get; set; }
    }

    public record FullName(string FirstName, string LastName);
}

