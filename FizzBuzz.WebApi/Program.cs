using FizzBuzz.Application.Interfaces;
using FizzBuzz.Application.Services;
using FizzBuzz.Infrastructure.Persistence;
using FizzBuzz.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Application layer services
builder.Services.AddScoped<IFizzBuzzService, FizzBuzzAppService>();

// Infrastructure layer services
builder.Services.AddSingleton<IStatisticsRepository, InMemoryStatisticsRepository>();
builder.Services.AddScoped<IStatisticsService, StatisticsService>();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactApp",
        policy => policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("ReactApp");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
