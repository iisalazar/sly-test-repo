using SlydynBackend.Middlewares;

namespace SlydynBackend.Extensions;

public static class AppExtension
{
  public static IApplicationBuilder UseAuthSessionMiddleware(this IApplicationBuilder builder)
  {
    return builder.UseMiddleware<AuthSessionMiddleware>();
  }
}