using JELMusic.Application;
using JELMusic.Framework;
using JELMusic.Infrastructure;
using JELMusic.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddApplication();

builder.Services.AddJELMusicFramework();

builder.Services.AddInfrastructure(
    "Data Source=jelmusic-core.db");

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CoreDbContext>();
    db.Database.EnsureCreated();
}

Console.WriteLine("JEL-Music CORE running");