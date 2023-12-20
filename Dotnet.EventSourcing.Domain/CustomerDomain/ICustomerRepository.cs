using System;
namespace Dotnet.EventSourcing.Domain.CustomerDomain
{
	public interface ICustomerRepository
	{
		Task<Customer> GetCustomerById(Guid customerId);
	}
}

