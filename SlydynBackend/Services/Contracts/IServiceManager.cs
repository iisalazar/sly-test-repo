namespace Services.Contracts;

public interface IServiceManager
{
  IAuthenticationService AuthService { get; }
}