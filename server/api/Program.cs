using efscaffold.Entities;
using Infrastructure.Postgres.Scaffolding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MyDbContext>(conf =>
{
    conf.UseNpgsql("Server=ep-proud-sound-adtbyomv-pooler.c-2.us-east-1.aws.neon.tech;DB=neondb;UID=neondb_owner;PWD=npg_BnxYF7ukDLC6;SslMode=require");
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
