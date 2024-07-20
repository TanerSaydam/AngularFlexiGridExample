using Bogus;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Server.WebAPI.Context;
using Server.WebAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(action =>
{
    action.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddCors();

builder.Services.AddControllers().AddOData(action =>
{
    action.EnableQueryFeatures();
});


var app = builder.Build();

app.UseCors(x => x.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());

app.MapGet("/seedData", async (ApplicationDbContext context) =>
{
    List<User> users = new();

    for (int i = 0; i < 1000; i++)
    {
        Faker faker = new();
        User user = new()
        {
            Id = Guid.NewGuid(),
            FirstName = faker.Person.FirstName,
            LastName = faker.Person.LastName,
            DateOfBirth = DateOnly.FromDateTime(faker.Person.DateOfBirth),
            Salary = faker.Person.Random.Decimal(17002, 120000)
        };

        users.Add(user);
    }

    context.AddRange(users);
    await context.SaveChangesAsync();
    return Results.Created();
});

app.MapGet("/getall", async (ApplicationDbContext context) =>
{
    var users = await context.Users.ToListAsync();
    return users;
});

app.MapControllers();

app.Run();
