using System.Text;
using System.Security.Claims;
using Ticketing.Common.Models;
using Ticketing.Domain.Entities;
using Ticketing.Common.Utilities;
using Ticketing.Domain.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Ticketing.Application.Models.DTOs;
using Ticketing.Domain.Models.Configurations;
using Ticketing.Application.Common.Contracts;
using Ticketing.Application.Models.DTOs.Users;

namespace Ticketing.Application.Services;

public class UserService
    (IUserRepository userRepository, IUnitOfWork uow, SiteSettings siteSettings)
    : IUserService
{
    public async Task<OperationResult<RegisterResponseDto>> CreateAsync(RegisterRequestDto request, CancellationToken cancellationToken)
    {
        var passwordHash = SecurityUtility.GetSha256Hash(request.Password);
        var user = User.Create(request.FullName, request.Email, passwordHash, request.Role);
        userRepository.Create(user);
        var saveChangesResult = await uow.SaveChangesAsync(cancellationToken);
        if (!saveChangesResult.IsSuccess)
            return OperationResult<RegisterResponseDto>.Fail();

        return new RegisterResponseDto
        {
            Id = user.Id,
            Role = user.Role,
            Email = user.Email,
            FullName = user.FullName,
            Password = user.Password
        };
    }

    public async Task<OperationResult<LoginResponseDto>> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken)
    {
        var passwordHash = SecurityUtility.GetSha256Hash(request.Password);
        var user = await userRepository
            .FindByUserNamePasswordAsync(request.Email, passwordHash, cancellationToken);

        if (user is null)
            return ErrorModel.Create("UserNameOrPasswordIsNotValid", "نام کاربری یا رمز عبور صحیح نمی باشد.");

        var tokenResponse = GenerateToken(user);

        var saveChangesResult = await uow.SaveChangesAsync(cancellationToken);
        if (!saveChangesResult.IsSuccess)
            return OperationResult<LoginResponseDto>.Fail();

        var result = new LoginResponseDto
        {
            ExpiresIn = tokenResponse.Result!.ExpiresIn,
            TokenType = tokenResponse.Result!.TokenType,
            AccessToken = tokenResponse.Result!.AccessToken
        };
        return result;
    }

    public async Task<OperationResult<UserDto?>> GetByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(userId, cancellationToken);
        if (user is null)
            return ErrorModel.Create("InvalidId", "شناسه نامعتبر");

        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Role = user.Role,
            FullName = user.FullName
        };
    }

    public async Task<OperationResult<UserDto?>> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(email, cancellationToken);
        if (user is null)
            return ErrorModel.Create("InvalidEmail", "ایمیل نامعتبر");

        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Role = user.Role,
            FullName = user.FullName
        };
    }

    public async Task<OperationResult> DeleteAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(userId, cancellationToken);
        if (user is null)
            return ErrorModel.Create("InvalidId", "شناسه نامعتبر");

        userRepository.Delete(user);
        var saveChangesResult = await uow.SaveChangesAsync(cancellationToken);
        if (!saveChangesResult.IsSuccess)
            return OperationResult.Fail();

        return OperationResult.Success();
    }

    public async Task<OperationResult<UpdateUserResponseDto>> UpdateAsync(UpdateUserRequestDto request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.userId, cancellationToken);
        if (user is null)
            return ErrorModel.Create("InvalidId", "شناسه نامعتبر");

        user.UpdateEmail(request.Email);
        user.UpdateFullName(request.FullName);
        var saveChangesResult = await uow.SaveChangesAsync(cancellationToken);
        if (!saveChangesResult.IsSuccess)
            return OperationResult<UpdateUserResponseDto>.Fail();

        return new UpdateUserResponseDto
        {
            Id = user.Id,
            Email = user.Email,
            Role = user.Role,
            FullName = user.FullName
        };
    }

    private OperationResult<AccessTokenResponse> GenerateToken(User user)
    {
        var secretKey = Encoding.UTF8.GetBytes(siteSettings.JwtSettings.SecretKey); // longer that 16 character
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);
        var encryptionkey = Encoding.UTF8.GetBytes(siteSettings.JwtSettings.EncryptKey); //must be 16 character
        var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionkey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

        var claims = GenerateClaims(user);
        var descriptor = new SecurityTokenDescriptor
        {
            Issuer = siteSettings.JwtSettings.Issuer,
            Audience = siteSettings.JwtSettings.Audience,
            IssuedAt = DateTime.UtcNow,
            NotBefore = DateTime.UtcNow.AddMinutes(siteSettings.JwtSettings.NotBeforeMinutes),
            Expires = DateTime.UtcNow.AddMinutes(siteSettings.JwtSettings.ExpirationMinutes),
            SigningCredentials = signingCredentials,
            EncryptingCredentials = encryptingCredentials,
            Subject = new ClaimsIdentity(claims)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateJwtSecurityToken(descriptor);
        return new AccessTokenResponse(securityToken);
    }

    private static List<Claim> GenerateClaims(User user)
    {
        var jwtId = Guid.NewGuid().ToString();
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Email),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Role, user.Role.ToString()),
            new(JwtRegisteredClaimNames.Jti, jwtId),
        };
        return claims;
    }
}