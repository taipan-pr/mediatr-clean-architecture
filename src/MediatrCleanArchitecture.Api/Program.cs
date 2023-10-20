using MediatrCleanArchitecture.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    // Setup configuration from appsettings.json files by environments
    // set in launchSettings.json => ASPNETCORE_ENVIRONMENT
    .UseAppConfigurations()

    // Setup SeriLog from configuration in appsettings.json
    .UseSeriLog();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
