using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Mockaroo.Services;
using MockarooBlazor.Data;
using MockarooLibrary.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
string connectionString = "Host=localhost;Port=5432;Database=Mockaroo;Username=postgres;Password=1312";
builder.Services.AddTransient<ITableEntityRepository, TableEntityRepository>(provider => new TableEntityRepository(connectionString));
builder.Services.AddTransient<IMockService, MockService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();