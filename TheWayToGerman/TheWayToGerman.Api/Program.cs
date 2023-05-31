using Microsoft.AspNetCore.HttpOverrides;
using System.Runtime.CompilerServices;
using TheWayToGerman.Core;
using TheWayToGerman.DataAccess;
using TheWayToGerman.Logic;

[assembly: InternalsVisibleTo("IntegrationTest")]

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddPostgresDB(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRepositories();
builder.Services.AddMediatR();
builder.Services.AddJWTAuth(builder.Configuration);
builder.Services.AddDataTimeProvider();
builder.Services.AddRateLimiters(builder.Configuration);
builder.Services.AddHealthChecks();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});
app.UseRateLimiter();
app.MapControllers();
app.MapHealthChecks("/health");
app.Run();
