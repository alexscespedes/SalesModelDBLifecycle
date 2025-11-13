using System;
using Microsoft.EntityFrameworkCore;
using SalesDataModel.Data;
using SalesDataModel.Tests.Environments;

namespace SalesDataModel.Tests;

public class SeederTests
{
    [Fact]
    public async Task Seeds_Development_Data_When_Empty()
    {
        var options = new DbContextOptionsBuilder<SalesDbContext>()
            .UseInMemoryDatabase("SeedTestDb")
            .Options;

        using var context = new SalesDbContext(options);
        await context.Database.EnsureCreatedAsync();

        var env = new FakeEnvironment("Development");

        await DataSeeder.SeedAsync(context, env);

        Assert.True(context.Customers.Any());
        Assert.True(context.Orders.Any());
    }
}
