using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.EventSourcing.SharedKernel
{
    public interface ICommand<TCommandData, TResult>
    {
        Task<TResult> ProcessCommandAsync(TCommandData commandData, EventId eventId, CancellationToken cancellationToken);
    }
}
