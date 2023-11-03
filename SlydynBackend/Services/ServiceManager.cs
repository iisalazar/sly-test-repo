using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repository.Contracts;
using Services.Contracts;

namespace Services;

public class ServiceManager: IServiceManager
{
  private readonly Lazy<IAuthenticationService> _authService;

  public ServiceManager(IRepositoryManager repository, UserManager<User> userManager, IConfiguration configuration,
    IMapper mapper)
  {
    _authService =
      new Lazy<IAuthenticationService>(() => new AuthenticationService(mapper, repository, userManager, configuration));
  }

  public IAuthenticationService AuthService => _authService.Value;

}