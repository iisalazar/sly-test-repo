using System.Security.Claims;
using System.Text;
using Entities.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository;
using Repository.Contracts;
using Services;
using Services.Contracts;

namespace SlydynBackend.Extensions;

public static class MainExtension
{
  public static void ConfigureCors(this IServiceCollection services) => services.AddCors(options =>
  {
    options.AddPolicy("CorsPolicy", builder =>
    {
      builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .WithOrigins("http://localhost:3000");
    });
  });

  public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
    => services.AddDbContext<RepositoryContext>(
      options => options.UseSqlServer(configuration.GetConnectionString("SqlConnection"))
    );

  public static void ConfigureIdentity(this IServiceCollection services)
  {
    var builder = services.AddIdentity<User, IdentityRole>(options =>
      {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequiredLength = 10;
        options.User.RequireUniqueEmail = true;
        options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
      }).AddEntityFrameworkStores<RepositoryContext>()
      .AddDefaultTokenProviders();
  }

  public static void ConfigureRepositoryManager(this IServiceCollection services) =>
    services.AddScoped<IRepositoryManager, RepositoryManager>();

  public static void ConfigureServiceManager(this IServiceCollection services) =>
    services.AddScoped<IServiceManager, ServiceManager>();


  public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
  {
    var jwtSettings = configuration.GetSection("JwtSettings");
    var secretKey = jwtSettings["SecretKey"];

    services.AddAuthentication(authOptions =>
    {
      authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(jwtOptions =>
    {
      jwtOptions.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["ValidIssuer"],
        ValidAudience = jwtSettings["ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))
      };

      jwtOptions.Events = new JwtBearerEvents
      {
        OnAuthenticationFailed = context =>
        {
          context.Response.StatusCode = StatusCodes.Status401Unauthorized;
          return context.Response.WriteAsync("Authentication failed: " + context.Exception.Message);
        },
        
      };
    });
  }

  public static void ConfigureSwagger(this IServiceCollection services)
  {
    services.AddSwaggerGen(swaggerOptions =>
    {
      swaggerOptions.SwaggerDoc("v1", new OpenApiInfo { Title = "SlydynBackend", Version = "v1" });

      swaggerOptions.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
      {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Please enter JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.Http
      });

      swaggerOptions.AddSecurityRequirement(new OpenApiSecurityRequirement
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
            {
              Type = ReferenceType.SecurityScheme,
              Id = "Bearer"
            },
            Name = "Bearer"
          },
          new List<string>()
        }
      });
    });
  }
}