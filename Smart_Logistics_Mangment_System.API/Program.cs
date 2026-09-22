using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Smart_Logistics_Mangment_System.API.Hubs;
using Smart_Logistics_Mangment_System.API.Services;
using Smart_Logistics_Mangment_System.Application.Extentions;
using Smart_Logistics_Mangment_System.Application.Notifications;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Infrastruction.Extentions;
using System.Text;
using System.Text.Json.Serialization;

namespace Smart_Logistics_Mangment_System.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // -------------------- Services --------------------

            // Controllers
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(
                        new JsonStringEnumConverter()
                    );
                });
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
            builder.Services.AddScoped<INotificationService,NotificationService>();


            // -------------------- Swagger + JWT --------------------

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Smart Logistics Management System API",
                    Version = "v1"
                });

                // JWT
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",

                    Type = SecuritySchemeType.Http,

                    Scheme = "bearer",

                    BearerFormat = "JWT",

                    In = ParameterLocation.Header,

                    Description = "Enter your JWT token."
                });

                c.AddSecurityRequirement(document =>
                    new OpenApiSecurityRequirement
                    {
                        [new OpenApiSecuritySchemeReference("Bearer", document)] =
                            new List<string>()
                    });
            });


            // -------------------- Custom Extensions --------------------

            builder.Services.AddInfrastructure(
                builder.Configuration
            );

            builder.Services.ApplicationServices();


            // -------------------- JWT Authentication --------------------

            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var jwtSettings =
                        builder.Configuration.GetSection("Jwt");

                    options.TokenValidationParameters =
                            new TokenValidationParameters
                                {
                                    ValidateIssuer = true,

                                    ValidateAudience = true,

                                    ValidateLifetime = true,

                                    ValidateIssuerSigningKey = true,

                                    ValidIssuer = jwtSettings["Issuer"],

                                    ValidAudience = jwtSettings["Audience"],

                                    IssuerSigningKey = 
                                    new SymmetricSecurityKey(
                                    Encoding.UTF8.GetBytes(
                                    jwtSettings["Key"]!)),

                                    ClockSkew = TimeSpan.Zero
                           };
                });


            // -------------------- HttpContext --------------------

            builder.Services.AddHttpContextAccessor();


            // -------------------- CORS --------------------

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod());
            });

            builder.Services.AddSignalR();
            var app = builder.Build();
            app.UseStaticFiles();


            // -------------------- Middleware --------------------

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();

                app.UseSwaggerUI();
            }


            app.UseCors("AllowAll");

            app.UseHttpsRedirection();


            // JWT
            app.UseAuthentication();

            app.UseAuthorization();


            // Controllers
            app.MapControllers();

            app.MapHub<NotificationHub>("/notificationHub");


            app.Run();
        }
    }
}