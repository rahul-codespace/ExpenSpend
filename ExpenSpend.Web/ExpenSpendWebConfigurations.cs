﻿using System.Text;
using System.Text.Json.Serialization;
using ExpenSpend.Core.DTOs.Emails;
using ExpenSpend.Data.Context;
using ExpenSpend.Data.Repository;
using ExpenSpend.Domain;
using ExpenSpend.Domain.Models.Friends;
using ExpenSpend.Domain.Models.GroupMembers;
using ExpenSpend.Domain.Models.Groups;
using ExpenSpend.Domain.Models.Users;
using ExpenSpend.Service;
using ExpenSpend.Service.Emails;
using ExpenSpend.Service.Emails.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace ExpenSpend.Web;

public static class ExpenSpendWebConfigurations
{
    public static void AddDbContextConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ExpenSpendDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("AppDbContext") ?? throw new InvalidOperationException("Connection string 'AppDbContext' not found."));
        });
    }

    public static void AddControllerConfig(this IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals;
        });
    }
    public static void AddIdentityConfig(this IServiceCollection services)
    {
        services.AddIdentity<ESUser, IdentityRole<Guid>>(options => options.SignIn.RequireConfirmedEmail = true)
            .AddEntityFrameworkStores<ExpenSpendDbContext>()
            .AddDefaultTokenProviders();
    }

    public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = configuration["JWT:ValidAudience"],
                ValidIssuer = configuration["JWT:ValidIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!))
            };
        });
    }

    public static void AddEmailService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration.GetSection("EmailConfig").Get<EmailConfigurationDto>()!);
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IExpenSpendRepository<>), typeof(ExpenSpendRepository<>));
        services.AddScoped<IAuthAppService, AuthAppService>();
        services.AddScoped<IUserAppService, UserAppService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IFriendAppService, FriendAppService>();
        services.AddScoped<IGroupAppService, GroupAppService>();
        services.AddScoped<IGroupMemberAppService, GroupMemberAppService>();
        services.AddHttpContextAccessor();
    }
    
    public static void AddSwaggerConfig(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "ExpenSpend API", Version = "v1" });
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter your JWT token into the textbox below",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
            });
        });
    }
}