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

app.MapControllers();

app.UseOpenApi();
app.UseSwaggerUi();
await app.GenerateApiClientsFromOpenApi("/../../client/src/generated-ts-client.ts");
app.Run();