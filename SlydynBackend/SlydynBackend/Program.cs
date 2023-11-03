using System.Globalization;
using Microsoft.Extensions.Caching.Distributed;
using SlydynBackend.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRedis(builder.Configuration);
builder.Services.ConfigureSession();
builder.Services.ConfigureCors();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.UseSession();
app.UseAuthSessionMiddleware();
app.MapControllers();

app.Lifetime.ApplicationStarted.Register(() =>
{
  var currentTimeUtc = DateTime.UtcNow.ToString(CultureInfo.CurrentCulture);
  byte[] encodedCurrentTimeUtc = System.Text.Encoding.UTF8.GetBytes(currentTimeUtc);
  var options = new DistributedCacheEntryOptions()
    .SetSlidingExpiration(TimeSpan.FromSeconds(20));
  app.Services.GetService<IDistributedCache>()
    ?.Set("cachedTimeUTC", encodedCurrentTimeUtc, options);
});


app.Run();