using Markets.Services;
using Markets.Abstractions;
using Markets.Contexts;
using Markets.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Markets.JWTConfig;

namespace Markets
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<Product>();
            builder.Services.AddScoped<Category>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IEncryptService, EncryptService>();

            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")!);
            });

            builder.Services.AddSwaggerGen(
            options =>
            {
                options.AddSecurityDefinition(
                    JwtBearerDefaults.AuthenticationScheme,
                    new()
                    {
                        In = ParameterLocation.Header,
                        Description = "Please insert JWT with Bearer into field",
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "Jwt Token",
                        Scheme = JwtBearerDefaults.AuthenticationScheme
                    });

                options.AddSecurityRequirement(new() {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        new List<string>()
                    }
                });
            }
        );
            
        var jwt = builder.Configuration.GetSection("JWTConfig/JwtConfiguration").Get<JwtConfiguration>()
        ?? throw new Exception("JwtConfiguration not found");

                builder.Services.AddSingleton(provider => jwt);

                builder.Services.AddAuthorization();

                builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                           .AddJwtBearer(options =>
                           {
                               options.TokenValidationParameters = new()
                               {
                                   ValidateIssuer = true,
                                   ValidateAudience = true,
                                   ValidateLifetime = true,
                                   ValidateIssuerSigningKey = true,
                                   ValidIssuer = jwt.Issuer,
                                   ValidAudience = jwt.Audience,
                                   IssuerSigningKey = jwt.GetSigningKey()
                               };
                           });

                builder.Services.AddControllers();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}