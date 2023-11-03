using Entities.DTOs.Authentication;

namespace Repository.Contracts;

public interface IUserSessionRepository
{
  Task<Guid> CreateSessionAsync(UserDto userDto);
  Task<UserDto?> GetSessionAsync(Guid sessionId);
}