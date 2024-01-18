using Dotnet.EventSourcing.Domain.UserDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.EventSourcing.Application.DTOS
{
    public static class DTOMapper
    {
        public static UserDTO ToDTO(this User user)
        {
            return new UserDTO()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
            };
        }
    }
}
