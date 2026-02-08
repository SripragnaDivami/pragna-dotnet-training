using capstone_policy_management.Data;
using capstone_policy_management.Filters;
using capstone_policy_management.Helper;
using capstone_policy_management.Repository.Implementations;
using capstone_policy_management.Repository.Implwementations;
using capstone_policy_management.Repository.Interfaces;
using capstone_policy_management.Services.Implementations;
using capstone_policy_management.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<GlobalResponseFilter>();
builder.Services.AddScoped<PerformanceActionFilter>();
builder.Services.AddScoped<GlobalExceptionFilter>();

builder.Services.AddControllers(options =>
{
    options.Filters.AddService<GlobalExceptionFilter>();
    options.Filters.AddService<PerformanceActionFilter>();
});
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT token like: Bearer {your_token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

//Register AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Register JWT Token Generator
builder.Services.AddScoped<JwtTokenGenerator>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IPolicyService, PolicyService>();
builder.Services.AddScoped<IPolicyRepository, PolicyRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services .AddScoped<IPolicyEnrollmentService, PolicyEnrollmentService>();
builder.Services.AddScoped<IPolicyEnrollmentRepository, PolicyEnrollmentRepository>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("policymanagementconnectionstrings")));


var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"];

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
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)),
        RoleClaimType = System.Security.Claims.ClaimTypes.Role
    };
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
