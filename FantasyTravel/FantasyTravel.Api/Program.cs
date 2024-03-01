using Microsoft.AspNetCore.DataProtection.Repositories;
using FantasyTravel.Data;
using System.IO;
using FantasyTravel.Logic;

var builder = WebApplication.CreateBuilder(args);

// CHANGE THIS FILEPATH AS NEEDED
string _path = @".\..\connectionstring";
string uri1 = "https://api.weather.gov/gridpoints/ALY";
string uri2 = "";

// Add services to the container.
string connectionString = "";
if (File.Exists(_path))
{
    connectionString = File.ReadAllText(_path);
}
else
{
    //throw new ArgumentNullException(nameof(connectionString));
    Console.WriteLine("File not found!!!");
}

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IRepository>(sp => new SqlRepository(connectionString, sp.GetRequiredService<ILogger<SqlRepository>>()));
builder.Services.AddSingleton<JsonRepository>(wc => new JsonRepository(uri1, uri2, wc.GetRequiredService<ILogger<JsonRepository>>()));

var app = builder.Build();

// Initialize BiomeType linking table with "current" temperature reference values.
IEnumerable<Biome> biomes = await app.Services.GetService<IRepository>().GetAllBiomesAsync();
foreach (Biome b in biomes)
{
    Weather weather = await app.Services.GetService<JsonRepository>().GetWeatherByCoordinatesAsync(b.x, b.y);
    await app.Services.GetService<IRepository>().UpdateBiomeTemperatureAsync(b.id, Math.Round((weather.temperature * -1.8) + 32, 2));
}


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
