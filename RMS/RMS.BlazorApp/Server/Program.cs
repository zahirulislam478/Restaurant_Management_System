using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RMS.BlazorApp.HostedServices.AppDb;
using RMS.BlazorApp.HostedServices.RestaurantDb;
using RMS.BlazorApp.HostedServices;
using RMS.BlazorApp.Models;
using System.Text;
using RMS.BlazorApp.Repositories.Interfaces;
using RMS.BlazorApp.Repositories;

var builder = WebApplication.CreateBuilder(args);

#region DbContext
builder.Services.AddDbContext<RestaurantDbContext>(op => op.UseSqlServer(
    builder.Configuration.GetConnectionString("db"), b => b.MigrationsAssembly("RMS.BlazorApp.Server")));
builder.Services.AddDbContext<AppDbContext>(op => op.UseSqlServer(
    builder.Configuration.GetConnectionString("identity")));
#endregion

#region Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
#endregion

#region Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Authorization string as following: `Bearer Generated-JWT-Token`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                },
                 new List<string>()
            }
        });
});
#endregion

#region Identity Configuration
builder.Services.AddIdentity<IdentityUser, IdentityRole>(op =>
{
    op.Password.RequiredLength = 6;
    op.Password.RequireDigit = false;
    op.Password.RequireNonAlphanumeric = false;
    op.Password.RequireUppercase = false;
    op.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();
#endregion

#region Authentication/JWT Configuration
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidAudience = builder.Configuration["Jwt:Site"],
            ValidIssuer = builder.Configuration["Jwt:Site"],
            IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SigningKey"] ?? "IsDB-BISEW ESAD-54"))
        };
    });
#endregion

#region Hosted Services
builder.Services.AddScoped<ApplyMigrationService>();
builder.Services.AddHostedService<MigrationHostedService>();

builder.Services.AddScoped<MigrationService>();
builder.Services.AddHostedService<AppMigrationHostedService>();

builder.Services.AddScoped<IdentityDbInitializer>();
builder.Services.AddHostedService<IdentityInitializerHostedService>();
#endregion

#region Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCORS",
        builder =>
        {
            builder
                .WithOrigins("http://localhost:4200", "http://localhost:44452", "http://localhost:5249")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .Build();
        });
});
#endregion

#region AddNewtonsoftJson 
builder.Services.AddControllers().AddNewtonsoftJson(
    settings =>
    {
        settings.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
        settings.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
    }
    );
#endregion

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseBlazorFrameworkFiles();
app.UseCors("EnableCORS");
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
