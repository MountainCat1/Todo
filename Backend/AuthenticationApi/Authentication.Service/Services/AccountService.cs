﻿using Authentication.Domain.Entities;
using Authentication.Domain.Repositories;
using Authentication.Service.Errors;
using Authentication.Service.Models;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Service.Services;

public interface IAccountService
{
    public Task<Account> RegisterAsync(Guid userGuid, string password);
    public Task<AuthenticationResultModel> Authenticate(Guid userGuid, string password);
}

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Account> RegisterAsync(Guid userGuid, string password)
    {
        if (await _accountRepository.GetOneAsync(x => x.UserGuid == userGuid) is not null)
            throw new ArgumentException("Cannot create two accounts for one user GUID");

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        if (hashedPassword == null)
            throw new Exception("Hashing password failed");

        var account = new Account()
        {
            PasswordHash = hashedPassword,
            UserGuid = userGuid
        };

        var createdEntity = await _accountRepository.CreateAsync(account);

        return createdEntity;
    }

    public async Task<AuthenticationResultModel> Authenticate(Guid userGuid, string password)
    {
        var account = await _accountRepository.GetOneAsync(x => x.UserGuid == userGuid);

        if (account is null)
            throw new NotFoundError("Account not found");
        
        var verifySucceeded = BCrypt.Net.BCrypt.Verify(account.PasswordHash, password);

        if (verifySucceeded)
            return new AuthenticationResultModel()
            {
                Account = account,
                Result = AuthenticationResultModel.AuthenticationResult.Success
            };
        else
            return new AuthenticationResultModel()
            {
                Result = AuthenticationResultModel.AuthenticationResult.Failed
            };
    }
}