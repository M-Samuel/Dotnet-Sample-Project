using System;
using UserDomain = Dotnet.EventSourcing.Domain.UserDomain;
namespace Dotnet.EventSourcing.Infrastructure.DTO.UserDTO
{
	public static class DomainMapper
	{
		public static User ToDTO(this UserDomain.User user)
		{
			User userDTO = new User()
			{
				Id = user.Id,
				FirstName = user.FullName.FirstName,
				LastName = user.FullName.LastName
			};
			return userDTO;
		}

		public static UserDomain.User ToDomain(this User userDTO)
		{
			UserDomain.User user = new()
			{
				Id = userDTO.Id,
				FullName = new(userDTO.FirstName, userDTO.LastName)
			};
			return user;
        }
	}
}

