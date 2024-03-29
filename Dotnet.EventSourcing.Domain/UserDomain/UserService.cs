﻿using System;
using System.Linq;
using Dotnet.EventSourcing.Domain.UserDomain.UserDomainEvents;
using Dotnet.EventSourcing.Domain.UserDomain.UserErrors;
using Dotnet.EventSourcing.SharedKernel;

namespace Dotnet.EventSourcing.Domain.UserDomain
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;

        }

        public async Task<Result<User>> ProcessDomainEvent(CreateUserEvent createUserEvent, CancellationToken cancellationToken)
        {
            User? duplicateUser = await _userRepository.GetUserByNameAsync(createUserEvent.FirstName, createUserEvent.LastName, cancellationToken);

            Result<User> result = Result<User>.Create();
            result
                .AddErrorIf(() => duplicateUser != null, new UserAlreadyExistsError($"Cannot create user, {duplicateUser?.FirstName}, {duplicateUser?.LastName} already exists"))
                .AddErrorIf(() => string.IsNullOrWhiteSpace(createUserEvent.FirstName), new UserFirstNameEmptyError("FirstName cannot be null"))
                .AddErrorIf(() => string.IsNullOrWhiteSpace(createUserEvent.LastName), new UserLastNameEmptyError("LastName cannot be null"));

            if (result.HasError)
                return result;

            User user = User.Create(createUserEvent.FirstName, createUserEvent.LastName);
            await _userRepository.CreateUserAsync(user, cancellationToken);
            result.UpdateValueIfNoError(user);

            return result;
        }
    }
}

