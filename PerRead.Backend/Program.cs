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
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(
    options => options.AddDefaultPolicy(
        builder => builder
        // .WithOrigins("http://localhost:4200")
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()));

// Add services to the container.

builder.Services.AddControllers(
    options => options.Filters.Add<LogAsyncResourceFilter>()) // Add a global filter to all controllers
    .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT containing userid claim",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
        }); var security =
             new OpenApiSecurityRequirement
             {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                },
                UnresolvedReference = true
            },
            new List<string>()
        }
             };
        options.AddSecurityRequirement(security);
    });

AddServices(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors();

app.UseAuth();

app.MapControllers();

app.Run();

static void AddServices(WebApplicationBuilder builder)
{
    builder.Services.AddDbContext<AppDbContext>(
        options => options.UseSqlite(builder.Configuration.GetConnectionString("PerReadLocalDb")));

    // Services
    builder.Services.AddScoped<IArticleService, ArticleService>();
    builder.Services.AddScoped<IAuthService, AuthService>();
    builder.Services.AddScoped<IAuthorsService, AuthorsService>();
    builder.Services.AddScoped<ITagService, TagService>();
    builder.Services.AddScoped<IImageService, ImageService>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IPaymentService, PaymentService>();
    builder.Services.AddScoped<IFeedsService, FeedsService>();
    builder.Services.AddScoped<IRequesterGetter, RequesterGetter>();

    // Repositories
    builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
    builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
    builder.Services.AddScoped<ITagRepository, TagRespository>();
    builder.Services.AddScoped<IUserPreferenceRepository, UserPreferenceRepository>();
    builder.Services.AddScoped<IFeedRepository, FeedRepository>();
    builder.Services.AddScoped<ISectionRepository, SectionRepository>();

    builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

    builder.Services.AddIdentityCore<ApplicationUser>()
        .AddEntityFrameworkStores<AppDbContext>();

    AddExtraIdentityStuff(builder);

    var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();

    builder.Services.AddAuth(jwtSettings);

    //ConfigureAuth(builder);
}

static void AddExtraIdentityStuff(WebApplicationBuilder builder)
{
    builder.Services.AddHttpContextAccessor();
    // Identity services
    builder.Services.TryAddScoped<IUserValidator<ApplicationUser>, UserValidator<ApplicationUser>>();
    builder.Services.TryAddScoped<IPasswordValidator<ApplicationUser>, PasswordValidator<ApplicationUser>>();
    builder.Services.TryAddScoped<IPasswordHasher<ApplicationUser>, PasswordHasher<ApplicationUser>>();
    builder.Services.TryAddScoped<ILookupNormalizer, UpperInvariantLookupNormalizer>();
    //builder.Services.TryAddScoped<IRoleValidator<TRole>, RoleValidator<TRole>>();
    // No interface for the error describer so we can add errors without rev'ing the interface
    builder.Services.TryAddScoped<IdentityErrorDescriber>();
    builder.Services.TryAddScoped<ISecurityStampValidator, SecurityStampValidator<ApplicationUser>>();
    builder.Services.TryAddScoped<ITwoFactorSecurityStampValidator, TwoFactorSecurityStampValidator<ApplicationUser>>();
    //builder.Services.TryAddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, UserClaimsPrincipalFactory<ApplicationUser, TRole>>();
    builder.Services.TryAddScoped<UserManager<ApplicationUser>>();
    builder.Services.TryAddScoped<SignInManager<ApplicationUser>>();
    //builder.Services.TryAddScoped<RoleManager<TRole>>();
}