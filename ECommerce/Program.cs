
using ECommerce.Data;
using ECommerce.Services.Interfaces;
using ECommerce.Services.Services;
using Microsoft.EntityFrameworkCore;

namespace ECommerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Add model services
            builder.Services.AddScoped<IProduct,ProductsService>();
            builder.Services.AddScoped<ICustomer, CustomersService>();
            builder.Services.AddScoped<IOrder, OrdersService>();
            builder.Services.AddScoped<IOrderItem, OrderItemService>();
            builder.Services.AddScoped<IAddress, AddressesService>();
            builder.Services.AddScoped<ITrackingDetail, TrackingDetailsService>();
            builder.Services.AddScoped<ICategory, CategoriesService>();
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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
