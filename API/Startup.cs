using API.ExceptionFilters;
using API.Extensions;
using Contracts;
using DataBase;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Repository;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers(config =>
            {
                config.Filters.Add(typeof(UnexpectedExceptionsFilter));
                config.Filters.Add(typeof(NotImplementedExceptionFilter));
                config.Filters.Add(typeof(NotFoundExceptionFilter));
                config.Filters.Add(typeof(SameAirportExceptionFilter));
                config.Filters.Add(typeof(SameFlightExceptionFilter));
                config.Filters.Add(typeof(DateFormatExceptionFilter));
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });

            services.ConfigureSqlContext(Configuration);
            services.ConfigureAuth();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAirportRepository, AirportRepository>();
            services.AddScoped<IFlightRepository, FlightRepository>();
            services.AddScoped<IAppDbContext, AppDbContext>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
