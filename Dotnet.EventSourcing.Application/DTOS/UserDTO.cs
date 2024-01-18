using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.EventSourcing.Application.DTOS
{
    public class UserDTO
    {
        public string? FirstName { get; internal set; }
        public string? LastName { get; internal set; }
        public Guid Id { get; internal set; }
    }
}
