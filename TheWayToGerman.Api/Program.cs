using TheWayToGerman.Core;
using TheWayToGerman.Core.Entities;
using TheWayToGerman.DataAccess;
using TheWayToGerman.Logic;





bool fn(BaseEntity entity)
{

    return entity.DeleteDate == null;
}


var entites = typeof(BaseEntity).Assembly.GetTypes().Where(x => x.IsSubclassOf(typeof(BaseEntity)));

foreach (var entite in entites)
{
    fn(new User() { Email="",Name="",Username="" });

}


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddPostgresDB(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRepositories();
builder.Services.AddMediatR();
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
