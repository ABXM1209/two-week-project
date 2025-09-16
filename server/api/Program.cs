using api;
using efscaffold.Entities;
using Infrastructure.Postgres.Scaffolding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var appOptions = builder.Services.AddAppOptions(builder.Configuration);

builder.Services.AddDbContext<MyDbContext>(conf =>
{
    conf.UseNpgsql(appOptions.ConnectionString);
});

var app = builder.Build();

app.MapGet("/", (
    
    [FromServices]IOptionsMonitor<AppOptions> optionsMonitor,
    [FromServices]MyDbContext dbContext) =>
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
