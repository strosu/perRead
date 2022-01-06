using PerRead.Backend.Repositories;
using PerRead.Backend.Services;
using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Filters;
using Microsoft.AspNetCore.Identity;
using PerRead.Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PerRead.Backend.Models.Auth;
using PerRead.Backend.Extensions;

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

    builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

    builder.Services.AddIdentityCore<ApplicationUser>()
        .AddEntityFrameworkStores<AppDbContext>();

    var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();

    builder.Services.AddAuth(jwtSettings);

    //ConfigureAuth(builder);
}

static void ConfigureJwt(WebApplicationBuilder builder)
{
}

static void ConfigureAuth(WebApplicationBuilder builder)
{
    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
            options =>
            {
                //options.LoginPath = new PathString("/user/login");
                //options.AccessDeniedPath = new PathString("/auth/denied");
            });

    builder.Services.AddAuthorization(options =>
    {
        options.FallbackPolicy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();

        // Register other policies here
    });

    // ===== Configure Identity =======
    builder.Services.ConfigureApplicationCookie(options =>
    {
        options.Cookie.Name = "auth_cookie";
        options.Cookie.SameSite = SameSiteMode.None;
        options.LoginPath = new PathString("/api/contests");
        options.AccessDeniedPath = new PathString("/api/contests");

        // Not creating a new object since ASP.NET Identity has created
        // one already and hooked to the OnValidatePrincipal event.
        // See https://github.com/aspnet/AspNetCore/blob/5a64688d8e192cacffda9440e8725c1ed41a30cf/src/Identity/src/Identity/IdentityServiceCollectionExtensions.cs#L56
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };
    });
}