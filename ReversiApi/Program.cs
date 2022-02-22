global using ReversiApi.Repository;
global using Microsoft.AspNetCore.Mvc;
global using ReversiApi.Model.Game;
global using ReversiApi.Model.Game.DataTransferObject;
global using ReversiApi.Model.Player;
global using ReversiApi.Model.Player.DataTransferObject;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//services cors
builder.Services.AddCors(p => p.AddPolicy("corsapp", corsPolicyBuilder =>
{
    corsPolicyBuilder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddSingleton<IGamesRepository, GamesRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corsapp");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();