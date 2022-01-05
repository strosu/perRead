using PerRead.Backend.Repositories;
using PerRead.Backend.Services;
using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Filters;
using Microsoft.AspNetCore.Identity;
using PerRead.Backend.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(
    options => options.AddDefaultPolicy(
        builder => builder
        .WithOrigins("http://localhost:3000")
        .AllowAnyMethod()
        .AllowAnyHeader()));

// Add services to the container.

builder.Services.AddControllers(
    options => options.Filters.Add<LogAsyncResourceFilter>()) // Add a global filter to all controllers
    .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

AddServices(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

static void AddServices(WebApplicationBuilder builder)
{
    builder.Services.AddDbContext<AppDbContext>(
        options => options.UseSqlite(builder.Configuration.GetConnectionString("PerReadLocalDb")));

    builder.Services.AddScoped<IArticleService, ArticleService>();
    builder.Services.AddScoped<IArticleRepository, ArticleRepository>();


    builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<AppDbContext>();
}