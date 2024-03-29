﻿using System;
using Dotnet.EventSourcing.SharedKernel;

namespace Dotnet.EventSourcing.Domain.UserDomain
{
    public partial class User : IEntity
    {
        public Guid Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public static User Create(string firstName, string lastName)
        {
            User newUser = new()
            {
                FirstName = firstName,
                LastName = lastName
            };
            return newUser;
        }
    }

    public record FullName(string FirstName, string LastName);
}

