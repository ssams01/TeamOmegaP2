using FantasyTravel.Data;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// CHANGE THIS FILEPATH AS NEEDED
string _path = @".\..\connectionstring";

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
