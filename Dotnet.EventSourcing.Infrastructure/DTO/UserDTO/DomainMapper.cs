using System;
using UserDomain = Dotnet.EventSourcing.Domain.UserDomain;
namespace Dotnet.EventSourcing.Infrastructure.DTO.UserDTO
{
	public static class DomainMapper
	{
		public static User ToDTO(this UserDomain.User user)
		{
			User userDTO = new()
			{
				Id = user.Id,
				FirstName = user.FirstName ?? string.Empty,
				LastName = user.LastName ?? string.Empty
			};
			return userDTO;
		}

		public static UserDomain.User ToDomain(this User userDTO)
		{
			UserDomain.User user = new()
			{
				Id = userDTO.Id,
				FirstName = userDTO.FirstName,
				LastName = userDTO.LastName
			};
			return user;
		}
	}
}

