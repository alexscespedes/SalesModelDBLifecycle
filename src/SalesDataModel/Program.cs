using Microsoft.EntityFrameworkCore;
using SalesDataModel.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SalesDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<SalesDbContext>();
    DbInitializer.Initialize(context);
}

app.Run();
