using Microsoft.EntityFrameworkCore;
using SalesDataModel.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SalesDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SalesDbContext>();
    var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

    db.Database.Migrate();

    await DataSeeder.SeedAsync(db, env);
}

app.Run();