using shorturl.Authentication;
using Microsoft.EntityFrameworkCore;
using shorturl.Data;
using shorturl.Repository;
using shorturl.Dto;

var builder = WebApplication.CreateBuilder(args);
var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IShortUrlRepository, ShortUrlRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.WithOrigins(
                            "http://localhost:8080",
                            "http://localhost:5173"
                          )
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});
// Add services to the container.

builder.Services.AddDbContext<ShortUrlDBContext>(
    options => options.UseSqlite("name=ConnectionStrings:DefaultConnection")
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Authentication Service
builder.Services.AddScoped<AuthFilter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
