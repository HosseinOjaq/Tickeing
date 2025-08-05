using Ticketing.Common.Models;
using Ticketing.Application.Models.DTOs.Users;

namespace Ticketing.Application.Common.Contracts;

public interface IUserService
{
    Task<OperationResult<RegisterResponseDto>> CreateAsync(RegisterRequestDto request, CancellationToken cancellationToken);
    Task<OperationResult<LoginResponseDto>> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken);
    Task<OperationResult<UserDto?>> GetByIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<OperationResult<UserDto?>> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<OperationResult<UpdateUserResponseDto>> UpdateAsync(UpdateUserRequestDto request, CancellationToken cancellationToken);
    Task<OperationResult> DeleteAsync(Guid userId, CancellationToken cancellationToken);
}