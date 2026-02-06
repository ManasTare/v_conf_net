using Microsoft.EntityFrameworkCore;
using v_conf_net.Models;
using v_conf_net.Services;
using v_conf_net.Services.Interfaces;   // or the namespace where AppDbContext exists

namespace v_conf_net
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

            builder.Services.AddScoped<ILookupService, LookupService>();
            builder.Services.AddScoped<IDefaultConfigService, DefaultConfigService>();
            builder.Services.AddScoped<IAuthService, AuthService>(); // Register Auth Service using v_conf_net.Services;
            builder.Services.AddScoped<IInvoiceService, InvoiceService>(); // Register Invoice Service (Monolith)
            builder.Services.AddScoped<IUserService, UserService>(); // Register User Service for Registration
            builder.Services.AddScoped<IVehicleConfigService, VehicleConfigService>(); // Register Vehicle Config Service

            // Add JWT Authentication
            System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // Prevent remapping of claims (e.g. sub -> nameidentifier)
            var key = System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!);
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key)
                };
            });



            builder.Services.AddControllers();

            // Add CORS 
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:5173") // React Vite Server
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // app.UseHttpsRedirection(); // Disabled to prevent warning: Failed to determine the https port for redirect.

            app.UseCors("AllowReactApp"); // Must be before Auth

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.MapControllers();

            app.Run();
        }
    }
}
