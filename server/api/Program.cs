using efscaffold.Entities;
using Infrastructure.Postgres.Scaffolding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MyDbContext>(conf =>
{
    DotNetEnv.Env.Load(".env");
    var connStr = Environment.GetEnvironmentVariable("CONN_STR");
    conf.UseNpgsql(connStr);
});

var app = builder.Build();

app.MapGet("/", ([FromServices]MyDbContext dbContext) =>
{
    var myAuthor = new Author()
    {
        Id = Guid.NewGuid().ToString(),
        Name = "Charles Dickens",
        Createdat = DateTime.UtcNow
    };
    dbContext.Authors.Add(myAuthor);
    dbContext.SaveChanges();
    var authors = dbContext.Authors.ToList();
    return authors;
});

app.Run();
