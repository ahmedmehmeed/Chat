using ChattingApp.Domain.Models;
using ChattingApp.Helper.Security.Tokens;
using ChattingApp.Helper.Third_Party;
using ChattingApp.Helper.Third_Party_Settings;
using ChattingApp.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace ChattingApp.Installers
{
    public class InfrastructureInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("ChattingPolicy", builder =>
                builder
               // .AllowAnyOrigin()
                .AllowCredentials()
                .AllowAnyMethod()
                .WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                );
            });

            //configuration mapping between setting section to  class 
            services.Configure<JWT>(configuration.GetSection("JWT"));
            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
            services.Configure<SmtpSettings>(configuration.GetSection("MessagerSetting:MailSettings"));

            //login with google
            services.AddAuthentication().AddGoogle((opt =>
            {
                IConfigurationSection googleConfig = configuration.GetSection("Authentication:googleAuth");
                opt.ClientId = googleConfig["ClientId"];
                opt.ClientSecret = googleConfig["ClientSecret"];
            }));

            //config identity 
            services.AddIdentity<AppUsers, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = false;
                //options.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddFluentValidation(options =>
            {
                options.ImplicitlyValidateChildProperties = true;
                options.ImplicitlyValidateRootCollectionElements = true;
                options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(opt =>
               {
                   opt.RequireHttpsMetadata = false;
                   opt.SaveToken = false;
                   opt.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidIssuer = configuration["JWT:Issuer"],
                       ValidAudience = configuration["JWT:Audience"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
                       //expire token in same second don't wait 
                       ClockSkew = TimeSpan.Zero,

                   };

                   //options to add token signalR request
                   opt.Events = new JwtBearerEvents
                   {
                       OnMessageReceived = context =>
                       {
                           var accessToken = context.Request.Query["access_token"];
                           var path = context.HttpContext.Request.Path;
                           if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
                           {
                               context.Token = accessToken;
                           }
                           return Task.CompletedTask;
                       }
                   };
                   
               });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequiredAdminRole", policy => policy.RequireRole("Admin"));
                options.AddPolicy("ModeratePhotoRole", policy => policy.RequireRole("Admin","Moderator"));

            });
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Chatting App",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                 }
            },
                       new string[] {}
                      }
                  });
            });

            services.AddSignalR();

        }
    }
}
