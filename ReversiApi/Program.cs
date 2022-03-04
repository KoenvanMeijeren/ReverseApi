global using ReversiApi.Repository;
global using Microsoft.AspNetCore.Mvc;
global using ReversiApi.Model.Game;
global using ReversiApi.Model.Game.DataTransferObject;
global using ReversiApi.Model.Player;
global using ReversiApi.Model.Player.DataTransferObject;
using Microsoft.EntityFrameworkCore;
using ReversiApi.DataAccess;
using ReversiApi.Repository.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<GamesDataAccess>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("ReversiRestApiDatabase"))
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//services cors
builder.Services.AddCors(p => p.AddPolicy("corsapp", corsPolicyBuilder =>
{
    corsPolicyBuilder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddScoped<IGamesRepository, GamesDatabaseRepository>();
builder.Services.AddScoped<IPlayersRepository, PlayersDatabaseRepository>();

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