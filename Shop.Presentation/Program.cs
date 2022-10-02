using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Shop.Application.Repositories.v1;
using Shop.Application.Services.v1;
using Shop.Domain.Models.Identity;
using Shop.Infrastructure;
using Shop.Infrastructure.Mappers.v1;
using Shop.Infrastructure.Repositories.v1;
using Shop.Infrastructure.Services.v1;
using System.Globalization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApiVersioning();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtBearer:PrivateKey"])),
            ValidAudience = builder.Configuration["JwtBearer:Audience"],
            ValidIssuer = builder.Configuration["JwtBearer:Issuer"]
        };
    });

builder.Services
    .AddAuthorization();

builder.Services
    .AddAutoMapper(typeof(CustomProfile));

builder.Services
    .AddControllers();

builder.Services
    .AddDbContext<AppDbContext>(options =>
    {
        var connectionString = builder.Configuration.GetConnectionString("Database");
        options.UseNpgsql(connectionString);
    });

builder.Services
    .AddEndpointsApiExplorer();

builder.Services
    .AddIdentity<AppUser, AppRole>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services
    .AddScoped<ICategoryRepository, CategoryRepository>()
    .AddScoped<ILocationRepository, LocationRepository>()
    .AddScoped<IOrderProductRepository, OrderProductRepository>()
    .AddScoped<IOrderRepository, OrderRepository>()
    .AddScoped<IPictureRepository, PictureRepository>()
    .AddScoped<IProductRepository, ProductRepository>()
    .AddScoped<IProviderRepository, ProviderRepository>()
    .AddScoped<IStatusRepository, StatusRepository>()
    .AddScoped<ITransactionRepository, TransactionRepository>()
    .AddScoped<ICategoryService, CategoryService>()
    .AddScoped<ILocationService, LocationService>()
    .AddScoped<IOrderProductService, OrderProductService>()
    .AddScoped<IOrderService, OrderService>()
    .AddScoped<IPictureService, PictureService>()
    .AddScoped<IProductService, ProductService>()
    .AddScoped<IProviderService, ProviderService>()
    .AddScoped<IStatusService, StatusService>()
    .AddScoped<ITransactionService, TransactionService>();

builder.Services
    .AddSwaggerGen(options =>
    {
        var securityScheme = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Description =
                "Authorization header using the Bearer scheme.<br>" +
                "The 'Bearer[space]' prefix will be applied automatically. You must enter only the JWT into the text input.<br>" +
                "You can get the JWT from <i>user/login</i> and <i>user/register</i> endpoints.",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Scheme = JwtBearerDefaults.AuthenticationScheme,
            Reference = new OpenApiReference
            {
                Id = JwtBearerDefaults.AuthenticationScheme,
                Type = ReferenceType.SecurityScheme
            }
        };

        options.AddSecurityDefinition(securityScheme.Scheme, securityScheme);
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                securityScheme,
                Array.Empty<string>()
            }
        });

        // Avoid conflicts between classes that are using the same name in different namespaces
        options.CustomSchemaIds(type => type.FullName);
    });

builder.Services
    .Configure<RequestLocalizationOptions>(options =>
    {
        var defaultCulture = new CultureInfo(builder.Configuration.GetSection("Cultures:Default").Get<string>());
        var supportedCultures = builder.Configuration
            .GetSection("Cultures:Supported")
            .Get<string[]>()
            .Select(culture => new CultureInfo(culture))
            .ToList();

        foreach (var supportedCulture in supportedCultures)
        {
            supportedCulture.NumberFormat.NegativeSign = "-";
        }

        options.DefaultRequestCulture = new RequestCulture(defaultCulture);
        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;
    });

var app = builder.Build();

using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
using var appDbContext = serviceScope.ServiceProvider.GetService<AppDbContext>();

// Avoid seeding data in case of testing
if (appDbContext == null || appDbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory")
{
    return;
}

using var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<AppRole>>();
using var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();

if (builder.Configuration.GetSection("Seeding:DatabaseDrop").Get<bool>())
{
    Shop.Infrastructure.Seeding.Seeding.DatabaseDrop(appDbContext);
}

if (builder.Configuration.GetSection("Seeding:DatabaseMigrate").Get<bool>())
{
    Shop.Infrastructure.Seeding.Seeding.DatabaseMigrate(appDbContext);
}

if (builder.Configuration.GetSection("Seeding:SeedData").Get<bool>())
{
    Shop.Infrastructure.Seeding.Seeding.SeedData(appDbContext);
}

if (builder.Configuration.GetSection("Seeding:SeedIdentity").Get<bool>())
{
    var email = builder.Configuration.GetSection("Seeding:Email").Get<string>();
    var password = builder.Configuration.GetSection("Seeding:Password").Get<string>();

    if (roleManager == null)
    {
        throw new NullReferenceException("RoleManager cannot be null");
    }

    if (userManager == null)
    {
        throw new NullReferenceException("UserManager cannot be null");
    }

    Shop.Infrastructure.Seeding.Seeding.SeedIdentity(userManager, roleManager, email, password);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseRequestLocalization();

app.UseHttpsRedirection();

app.UseCors(b => b.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();