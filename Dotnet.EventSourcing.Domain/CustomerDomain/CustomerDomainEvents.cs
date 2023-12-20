using System;
using Dotnet.EventSourcing.SharedKernel;

namespace Dotnet.EventSourcing.Domain.CustomerDomain
{
    public record CreatedCustomerDomainEvent(Guid Id, FullName FullName) : IDomainEvent
    {
        public DateTime OccurranceDateTime => DateTime.UtcNow;
    }
}

