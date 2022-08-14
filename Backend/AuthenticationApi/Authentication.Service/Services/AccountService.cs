﻿using Authentication.Domain.Entities;
using Authentication.Domain.Repositories;
using Authentication.Service.Errors;
using Authentication.Service.Models;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Service.Services;

public interface IAccountService
{
    public Task<Account> RegisterAsync(string username, string password);
    public Task<AuthenticationResult> AuthenticateAsync(string username,string password);
}

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Account> RegisterAsync(string username, string password)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        if (hashedPassword == null)
            throw new Exception("Hashing password failed");

        var account = new Account()
        {
            PasswordHash = hashedPassword,
            Username = username
        };

        var createdEntity = await _accountRepository.CreateAsync(account);

        await _accountRepository.SaveChangesAsync();
        
        return createdEntity;
    }

    public async Task<AuthenticationResult> AuthenticateAsync(string username, string password)
    {
        var account = await _accountRepository.GetOneAsync(x => x.Username == username);

        if (account is null)
            throw new NotFoundError("Account not found");
        
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
        var verifySucceeded = BCrypt.Net.BCrypt.Verify(account.PasswordHash, passwordHash);

        if (verifySucceeded)
            return new AuthenticationResult()
            {
                Account = account,
                Succeeded = true
            };
        else
            return new AuthenticationResult()
            {
                Succeeded = false
            };
    }
}