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

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(
    config => config
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin()
    .SetIsOriginAllowed(x => true)
);



// it's wrong to put this code here 
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

// use this line to map the controllers in your application instead
app.MapControllers();

app.Run();
