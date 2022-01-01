using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebApp.BLL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApp.BLL.Interfaces;
using WebApp.DAL.EF;
using WebApp.DAL.EF.Seeding;
using WebApp.DAL.Interfaces;
using WebApp.Domain.Identity;
using WebApp.Helpers.MVC;

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApiVersioning(options => { options.ReportApiVersions = true; });

builder.Services
    .AddAuthentication()
    .AddCookie(options => { options.SlidingExpiration = true; })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT:Key").Get<string>())),
            ValidIssuer = builder.Configuration.GetSection("JWT:Issuer").Get<string>(),
            ValidAudience = builder.Configuration.GetSection("JWT:Issuer").Get<string>(),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services
    .AddAuthorization();

builder.Services
    .AddAutoMapper(
        typeof(WebApp.API.DTO.v1.AutoMapper.CustomProfile),
        typeof(WebApp.BLL.DTO.AutoMapper.CustomProfile),
        typeof(WebApp.DAL.DTO.AutoMapper.CustomProfile)
    );

builder.Services
    .AddControllersWithViews(options =>
    {
        options.ModelBinderProviders.Insert(0, new WebApp.Helpers.MVC.FloatingPointModelBinderProvider());
    });

builder.Services
    .AddCors(options =>
    {
        options.AddPolicy("CorsAllowAll", builder =>
        {
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
            builder.AllowAnyOrigin();
        });
    });

builder.Services
    .AddDatabaseDeveloperPageExceptionFilter();

builder.Services
    .AddDbContext<AppDbContext>(options =>
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        options.UseSqlServer(connectionString, sqlServerOptions =>
        {
            sqlServerOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
        });
    });

builder.Services
    .AddIdentity<AppUser, AppRole>()
    .AddDefaultTokenProviders()
    .AddDefaultUI()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services
    .AddScoped<IAppBLL, AppBLL>()
    .AddScoped<IAppUnitOfWork, AppUnitOfWork>();

builder.Services
    .AddSingleton<IConfigureOptions<MvcOptions>, ConfigureOptions>();

builder.Services
    .AddSwaggerGen();

builder.Services
    .AddTransient<IConfigureOptions<SwaggerGenOptions>, WebApp.Helpers.Swagger.ConfigureOptions>();

builder.Services
    .AddVersionedApiExplorer(options => { options.GroupNameFormat = "'v'VVV"; });

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
    Seeding.DatabaseDrop(appDbContext);
}

if (builder.Configuration.GetSection("Seeding:DatabaseMigrate").Get<bool>())
{
    Seeding.DatabaseMigrate(appDbContext);
}

if (builder.Configuration.GetSection("Seeding:SeedData").Get<bool>())
{
    Seeding.SeedData(appDbContext);
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

    Seeding.SeedIdentity(userManager, roleManager, email, password);
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors("CorsAllowAll");

app.UseRouting();

app.UseRequestLocalization();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "Default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();