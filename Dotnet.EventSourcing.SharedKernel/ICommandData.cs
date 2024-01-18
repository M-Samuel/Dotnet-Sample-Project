using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.EventSourcing.SharedKernel
{
    public interface ICommandData<TEvent> where TEvent : IDomainEvent
    {
        TEvent ToEvent();
    }
}
