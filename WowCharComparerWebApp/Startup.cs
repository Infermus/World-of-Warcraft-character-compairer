using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WowCharComparerWebApp.Configuration;
using WowCharComparerWebApp.Data.Connection;
using WowCharComparerWebApp.Data.Database;
using WowCharComparerWebApp.Data.Database.Repository.Users;
using WowCharComparerWebApp.Logic.User;

namespace WowCharComparerWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging((logger) =>
            {
                logger.AddConfiguration(Configuration.GetSection("Logging"));
                logger.AddConsole();
            });

            services.AddDbContext<ComparerDatabaseContext>((builder) =>
            {
                if (Configuration.GetValue<string>("DatabaseType").Equals("PostgreSQL"))
                    builder.UseNpgsql(Configuration["ConnectionStrings:PostgreSqlConnection"]);
                else builder.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]);
            });

            services.AddMvc();
            services.AddTransient<IAPIDataRequestManager, APIDataRequestManager>();
            services.AddScoped<PasswordValidationManager>();
            services.AddScoped<DbAccessUser>();

            APIConf.WoWCharacterComparerEmailPassword = Configuration["WowCharacterComparerEmailPassword"];
            APIConf.WowCharacterComparerEmail = Configuration["WowCharacterComparerEmail"];
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // Adding css, js files
            app.UseStaticFiles();

            // MVC configuration
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
