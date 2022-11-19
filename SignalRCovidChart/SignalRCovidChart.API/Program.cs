using Microsoft.EntityFrameworkCore;
using SignalRCovidChart.API.Hubs;
using SignalRCovidChart.API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSignalR();

builder.Services.AddScoped<CovidService>();
builder.Services.AddDbContext<AppDbContext>(x =>
x.UseSqlServer(builder.Configuration.GetConnectionString("Conn"))

);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:5220", "http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials()
            .SetIsOriginAllowed((host) => true);
    });
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();
app.MapHub<CovidHub>("/CovidHub");

app.Run();
