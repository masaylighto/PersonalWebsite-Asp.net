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
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
