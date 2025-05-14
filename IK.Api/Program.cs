using IK.Application.Layer.Services.AvansTalebi;
using IK.Application.Layer.Services.BolumService;
using IK.Application.Layer.Services.HarcamaTalebi;
using IK.Application.Layer.Services.HarcamaTalebiService;
using IK.Application.Layer.Services.IzinTalebi;
using IK.Application.Layer.Services.IzinTalebiService;
using IK.Application.Layer.Services.KurumService;
using IK.Application.Layer.Services.LoginService;
using IK.Application.Layer.Services.PaketService;
using IK.Application.Layer.Services.PersonelService;
using IK.CoreLayer.Entities;
using IK.CoreLayer.Repositories.Abstract;
using IK.InfrastructureLayer;
using IK.InfrastructureLayer.EmailSender;
using IK.InfrastructureLayer.Repositories.Concretes;
using IK.ServiceLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure EF Core with SQL Server connection
builder.Services.AddDbContext<IKDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity with your custom ApplicationUser and Rol
builder.Services
    .AddIdentity<ApplicationUser, Rol>(options =>
    {
        options.SignIn.RequireConfirmedEmail = false;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
    })
    .AddEntityFrameworkStores<IKDBContext>()
    .AddDefaultTokenProviders();

// Dependency Injection for application services
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IBolumService, BolumService>();
builder.Services.AddScoped<IBolumRepository, BolumRepository>();
builder.Services.AddScoped<IPersonelService, PersonelService>();
builder.Services.AddScoped<IPersonelRepository, PersonelRepository>();
builder.Services.AddScoped<UserManager<ApplicationUser>>(); // Identity's UserManager
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IIzinTalebiService, IzinTalebiService>();
builder.Services.AddScoped<IIzinTalebiRepository, IzinTalebiRepository>();
builder.Services.AddScoped<IPaketService, PaketService>();
builder.Services.AddScoped<IPaketRepository, PaketRepository>();
builder.Services.AddScoped<IHarcamaTalebiService, HarcamaTalebiService>();
builder.Services.AddScoped<IHarcamaTalebiRepository, HarcamaTalebiRepository>();
builder.Services.AddScoped<IAvansTalebiService, AvansTalebiService>();
builder.Services.AddScoped<IAvansTalebiRepository, AvansTalebiRepository>();
builder.Services.AddScoped<IKurumService, KurumService>();
builder.Services.AddScoped<IKurumRepository, KurumRepository>();

builder.Services.AddTransient<IEmailSender, EmailSenderWithMailKit>();


// Configure Authentication with JWT Bearer
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])
        ),
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ClockSkew = TimeSpan.Zero
    };
});

// Configure CORS to allow all origins, methods, and headers (geliþtirme aþamasýnda)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
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

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();