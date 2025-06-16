using CMDM.Manager.Services.Interfaces;
using CMDM.Manager.Services;
using CMDM.DAL.Database;
using Microsoft.EntityFrameworkCore;
using CMDM.DAL.Repositories.Interfaces;
using CMDM.DAL.Repositories;

namespace CMDM.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowNextJsApp", builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            // Register services
            builder.Services.AddScoped<ICustomerDataService, CustomerDataService>();
            builder.Services.AddScoped<ICustomerMasterService, CustomerMasterService>();
            builder.Services.AddScoped<ICustomerReferenceService, CustomerReferenceService>();
            builder.Services.AddScoped<ICustomerMasterRepository, CustomerMasterRepository>();
            builder.Services.AddScoped<ICustomerReferenceRepository, CustomerReferenceRepository>();

            var app = builder.Build();

            app.UseCors("AllowNextJsApp");

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
