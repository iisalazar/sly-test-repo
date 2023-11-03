namespace SlydynBackend.Extensions;

public static class RedisExtension
{
  public static void ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddStackExchangeRedisCache(options =>
    {
      options.Configuration = configuration.GetConnectionString("RedisConnection");

      options.InstanceName = "SlydynRedisInstance";
    });
  }

  public static void ConfigureSession(this IServiceCollection services)
  {
    services.AddSession(options =>
    {
      options.IdleTimeout = TimeSpan.FromMinutes(30);
      options.Cookie.HttpOnly = true;
      options.Cookie.IsEssential = true;
    });
  }
}