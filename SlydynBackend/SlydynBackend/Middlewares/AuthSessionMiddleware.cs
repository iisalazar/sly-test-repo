using Services.Contracts;

namespace SlydynBackend.Middlewares;

public class AuthSessionMiddleware
{
  private readonly RequestDelegate _next;
  

  public AuthSessionMiddleware(RequestDelegate next)
  {
    _next = next;
  }

  public async Task InvokeAsync(HttpContext context, IServiceManager service)
  {
    
    var success = context.Request.Cookies.TryGetValue("sessionId", out var sessionId);

    if (!success)
    {
      await _next(context);
    }
    
    // attach user to context after getting session object 
    
      
    var result = await service.AuthService.GetUserSession(Guid.Parse(sessionId!));

    context.Session.SetString("User.Id", result.Id.ToString());
    if (result.UserName != null) context.Session.SetString("User.UserName", result.UserName);
    if (result.Email != null) context.Session.SetString("User.Email", result.Email);
    if(result.FirstName != null) context.Session.SetString("User.FirstName", result.FirstName);
    if(result.LastName != null) context.Session.SetString("User.LastName", result.LastName);

    await _next(context);
  }
}