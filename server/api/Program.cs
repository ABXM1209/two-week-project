using api;
using api.Services;
using efscaffold;
using efscaffold.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var appOptions = builder.Services.AddAppOptions(builder.Configuration);
builder.Services.AddScoped<ILibraryService, LibraryService>();
builder.Services.AddDbContext<MyDbContext>(conf =>
{
    conf.UseNpgsql(appOptions.ConnectionString);
});

builder.Services.AddControllers();
builder.Services.AddOpenApiDocument();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

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