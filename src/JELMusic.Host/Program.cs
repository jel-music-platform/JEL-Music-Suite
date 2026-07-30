using JELMusic.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<CoreDbContext>(options =>
{
    options.UseSqlite("Data Source=jelmusic-core.db");
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CoreDbContext>();
    db.Database.EnsureCreated();
}

Console.WriteLine("JEL-Music CORE running");