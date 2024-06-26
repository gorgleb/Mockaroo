using MockarooLibrary.Processors;
using MockarooLibrary.Processors.Interfaces;
using MockarooLibrary.Repository;
using MockarooLibrary.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = "Host=localhost;Port=5432;Database=Mockaroo;Username=postgres;Password=1312";
builder.Services.AddTransient<ITableEntityRepository, TableEntityRepository>(provider => new TableEntityRepository(connectionString));
builder.Services.AddTransient<IMockProcessor, MockProcessor>();

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