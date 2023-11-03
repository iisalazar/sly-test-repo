using System.Text.Json;
using System.Text.Json.Serialization;
using Entities.DTOs.Authentication;
using Microsoft.Extensions.Caching.Distributed;
using Repository.Contracts;

namespace Repository;

public class UserSessionRepository: IUserSessionRepository
{
  private readonly IDistributedCache _cache;
  
  public UserSessionRepository(IDistributedCache cache)
  {
    _cache = cache;
  }
  
  public async Task<Guid> CreateSessionAsync(UserDto userDto)
  {
    string userSerialized = JsonSerializer.Serialize(userDto);
    Guid sessionId = Guid.NewGuid();

    await _cache.SetStringAsync(sessionId.ToString(), userSerialized);

    return sessionId;
  }

  public async Task<UserDto?> GetSessionAsync(Guid sessionId)
  {
    var userSerialized = await _cache.GetStringAsync(sessionId.ToString());
    if (string.IsNullOrEmpty(userSerialized))
    {
      return null;
    }
    var userDeserialized = JsonSerializer.Deserialize<UserDto>(userSerialized!);

    return userDeserialized;
  }
}