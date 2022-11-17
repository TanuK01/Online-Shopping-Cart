using Core.Entities;
using eShoppingZoneAPI.Extensions;
using eShoppingZoneAPI.Helpers;
using eShoppingZoneAPI.Middleware;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace eShoppingZoneAPI
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddControllers();
            services.AddDbContext<DataContext>(x =>
                x.UseSqlite(_config.GetConnectionString("DefaultConnection")));
            services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var configuration = ConfigurationOptions.Parse(_config.GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(configuration);
            });
            services.AddDbContext<AppIdentityDbContext>(x =>
                {
                    x.UseSqlite(_config.GetConnectionString("IdentityConnection"));
                });
            services.AddApplicationServices();
//            services.Configure<SMTPConfig>(_config.GetSection("SMTPConfig"));
            services.AddIdentityServices(_config);
            services.AddSwagerDocumentation();
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            if (env.IsDevelopment())
            {
                
            }

            app.UseStatusCodePagesWithReExecute("/error/{0}");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSwaggerDocumentation();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
