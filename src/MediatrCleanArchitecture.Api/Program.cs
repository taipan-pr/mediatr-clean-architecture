using MediatrCleanArchitecture.Api;
using MediatrCleanArchitecture.Api.Extensions;
using MediatrCleanArchitecture.Api.Middlewares;
using MediatrCleanArchitecture.Application.Extensions;
using MediatrCleanArchitecture.Application.Interfaces;
using MediatrCleanArchitecture.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    // Setup configuration from appsettings.json files by environments
    // set in launchSettings.json => ASPNETCORE_ENVIRONMENT
    .UseAppConfigurations()

    // Setup SeriLog from configuration in appsettings.json
    .UseSeriLog()

    // Setup Autofac for DI container
    .UseAutofacProviderFactory<AutofacModule>();

// Add services to the container.
builder.Services
    // Add options
    .AddJsonOptions()
    .AddInfrastructureOptions(builder.Configuration)
    .AddApplicationOptions(builder.Configuration)

    // Configure options
    .Configure<RouteOptions>(options => options.LowercaseUrls = true)

    // Add services, all services are internal
    .AddInfrastructureServices()
    .AddPostgresDbContext(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")))
    .AddApplicationServices();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Run migrations and seed database if appsettings.json required
if(builder.Configuration.GetValue<bool>("Postgres:IsSeedingRequired"))
{
    using var scope = app.Services.CreateScope();
    var dataContext = scope.ServiceProvider.GetRequiredService<IDbContext>();
    await dataContext.SeedDatabase();
}

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<TimerMiddleware>();

app.UseMiddleware<RequestResponseLoggerMiddleware>();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
