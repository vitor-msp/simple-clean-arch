using SimpleCleanArch.Factory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDependencyInjections(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
