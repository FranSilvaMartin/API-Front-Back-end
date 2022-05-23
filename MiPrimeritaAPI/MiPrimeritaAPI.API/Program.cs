
using Microsoft.EntityFrameworkCore;
using MiPrimeritaAPI.BL.Contracts;
using MiPrimeritaAPI.BL.Implementations;
using MiPrimeritaAPI.CORE.Profiles;
using MiPrimeritaAPI.DAL;
using MiPrimeritaAPI.DAL.Contracts;
using MiPrimeritaAPI.DAL.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserBL, UserBL>();
builder.Services.AddScoped<IUserDAL, UserDAL>();

builder.Services.AddAutoMapper(cfg => cfg.AddProfile(new AutomapperProfile()));

builder.Services.AddDbContext<IESContext>();


//Para habilitar CORS en nuestra API
string[] exposedHeaders = { "Authorization" };
builder.Services.AddCors(o =>
{
    o.AddPolicy("AllowSetOrigins", options =>
    {
        options.WithOrigins("http://127.0.0.1:5500");
        options.AllowAnyHeader();
        options.AllowAnyMethod();
        options.AllowCredentials();
        options.WithExposedHeaders(exposedHeaders);

    });
});

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

app.UseCors("AllowSetOrigins");

app.Run();
