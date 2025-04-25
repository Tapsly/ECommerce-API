
using ECommerce.Authorization;
using ECommerce.Data;
using ECommerce.Services.Interfaces;
using ECommerce.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace ECommerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.
            // Add model services
            builder.Services.AddScoped<IProduct, ProductsService>();
            builder.Services.AddScoped<ICustomer, CustomersService>();
            builder.Services.AddScoped<IOrder, OrdersService>();
            builder.Services.AddScoped<IOrderItem, OrderItemService>();
            builder.Services.AddScoped<IAddress, AddressesService>();
            builder.Services.AddScoped<ITrackingDetail, TrackingDetailsService>();
            builder.Services.AddScoped<ICategory, CategoriesService>();
            // Add a response compression service
            builder.Services.AddResponseCompression();
            // Add services to the container.
            builder.Services.AddControllers().AddJsonOptions(
                options => options.JsonSerializerOptions.PropertyNamingPolicy = null
            );
            // Register the AutoMapper with dependency injection
            builder.Services.AddAutoMapper(typeof(Program).Assembly);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ECommerceDbContext>(
                options => options.UseSqlServer(
                    builder.Configuration.GetConnectionString("EFCoreDBConnection"))
            );
            // Add Caching services
            builder.Services.AddMemoryCache();

            // Add cors
            builder.Services.AddCors(options =>
                {
                    options.AddPolicy("AllowSpecificOrigin",
                        builder =>
                        {
                            builder.WithOrigins("http://localhost:3000")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowAnyHeader();

                        }
                    );
                }
            );
            var domain = $"https://{builder.Configuration["Auth0:Domain"]}/";
            // Adding authentication with jwt
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                    {
                        options.Authority = domain;
                        options.Audience = builder.Configuration["Auth0:Audience"];
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            NameClaimType = ClaimTypes.NameIdentifier
                        };
                    }

                );

            builder.Services.AddAuthorization(options =>
                {
                    options.AddPolicy("read:messages", policy =>
                        policy.Requirements
                            .Add(new HasScopeRequirement("read:messages", domain!)
                        )
                    );
                }
            );

            builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

            builder.Services.AddControllers();
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
            app.UseCors("AllowSpecificOrigin");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseResponseCompression();

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}
