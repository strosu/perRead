using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using PerRead.Backend.Extensions;
using PerRead.Backend.Filters;
using PerRead.Backend.Models;
using PerRead.Backend.Models.Auth;
using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Repositories;
using PerRead.Backend.Services;
using System.Text.Json.Serialization;

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
    .AddNewtonsoftJson(opt => opt.SerializerSettings.Converters.Add(new StringEnumConverter()));
;


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
//SeedCompanyEntities(app);
//SeedArticleOwnership(app);
app.Run();

static void AddServices(WebApplicationBuilder builder)
{
    builder.Services.AddDbContext<AppDbContext>(
        options => options.UseSqlite(builder.Configuration.GetConnectionString("PerReadLocalDb")).LogTo(Console.WriteLine, LogLevel.Information));

    // Services
    builder.Services.AddScoped<IArticleService, ArticleService>();
    builder.Services.AddScoped<IAuthService, AuthService>();
    builder.Services.AddScoped<IAuthorsService, AuthorsService>();
    builder.Services.AddScoped<ITagService, TagService>();
    builder.Services.AddScoped<IImageService, ImageService>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IFeedsService, FeedsService>();
    builder.Services.AddScoped<IRequesterGetter, RequesterGetter>();
    builder.Services.AddScoped<ISectionsService, SectionsService>();
    builder.Services.AddScoped<IRequestsService, RequestsService>();
    builder.Services.AddScoped<IPledgeService, PledgeService>();
    builder.Services.AddScoped<IWalletService, WalletService>();

    // Repositories
    builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
    builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
    builder.Services.AddScoped<ITagRepository, TagRespository>();
    builder.Services.AddScoped<IUserPreferenceRepository, UserPreferenceRepository>();
    builder.Services.AddScoped<IFeedRepository, FeedRepository>();
    builder.Services.AddScoped<ISectionRepository, SectionRepository>();
    builder.Services.AddScoped<IRequestsRepository, RequestsRepository>();
    builder.Services.AddScoped<IPledgeRepository, PledgeRepository>();
    builder.Services.AddScoped<IWalletRepository, WalletRepository>();
    builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

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

static void SeedCompanyEntities(WebApplication app)
{
    using (var serviceScope = app.Services.CreateScope())
    {
        var appDB = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
        var companyUser = appDB.Authors.FirstOrDefault(x => x.AuthorId == ModelConstants.CompanyAuthorId);

        if (companyUser == null)
        {
            var mainWallet = appDB.Wallets.FirstOrDefault(x => x.WalledId == ModelConstants.CompanyWalletId);

            if (mainWallet == null)
            {
                mainWallet = new Wallet
                {
                    WalledId = ModelConstants.CompanyWalletId,
                    TokenAmount = long.MaxValue
                };

                appDB.Wallets.Add(mainWallet);
            }

            var escrowWallet = appDB.Wallets.FirstOrDefault(x => x.WalledId == ModelConstants.CompanyEscrowWalletId);

            if (escrowWallet == null)
            {
                escrowWallet = new Wallet
                {
                    WalledId = ModelConstants.CompanyEscrowWalletId
                };

                appDB.Wallets.Add(escrowWallet);
            }

            appDB.SaveChanges();

            var user = new Author
            {
                AuthorId = ModelConstants.CompanyAuthorId,
                MainWallet = mainWallet,
                EscrowWallet = escrowWallet,
                Name = "PerRead company account"
            };

            user.MainWallet = mainWallet;
            user.EscrowWallet = escrowWallet;
            appDB.Authors.Add(user);

            mainWallet.Owner = user;
            escrowWallet.Owner = user;

            appDB.SaveChanges();
        }
    }
}

static void SeedArticleOwnership(WebApplication app)
{
    using (var serviceScope = app.Services.CreateScope())
    {
        var appDB = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();

        var articles = appDB.Articles.Include(x => x.ArticleAuthors)
            .ThenInclude(x => x.Author);

        foreach (var article in articles)
        {
            var authorCount = article.ArticleAuthors.Count();

            foreach (var author in article.ArticleAuthors)
            {
                var articleOwner = new ArticleOwner
                {
                    ArticleId = article.ArticleId,
                    AuthorId = author.AuthorId,
                    OwningPercentage = 1 / authorCount
                };

                appDB.ArticleOwners.Add(articleOwner);
            }
        }

        appDB.SaveChanges();
    }
}

static void SeedWallets(WebApplication app)
{
    using (var serviceScope = app.Services.CreateScope())
    {
        var appDB = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();

        foreach (var author in appDB.Authors)
        {
            if (string.IsNullOrEmpty(author.MainWalletId))
            {
                var wallet = new Wallet
                {
                    Owner = author,
                    WalledId = Guid.NewGuid().ToString()
                };

                appDB.Wallets.Add(wallet);
                author.MainWalletId = wallet.WalledId;
            }

            if (string.IsNullOrEmpty(author.EscrowWalletId))
            {
                var wallet = new Wallet
                {
                    Owner = author,
                    WalledId = Guid.NewGuid().ToString()
                };

                appDB.Wallets.Add(wallet);
                author.EscrowWalletId = wallet.WalledId;
            }
        }

        appDB.SaveChanges();
    }
}