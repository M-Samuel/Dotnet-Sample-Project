using System;
using System.ComponentModel.DataAnnotations;

namespace Dotnet.EventSourcing.Infrastructure.DTO.UserDTO
{
	public class User
	{
		[Key]
		public Guid Id { get; set; }
		public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}

