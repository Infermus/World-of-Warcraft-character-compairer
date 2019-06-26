using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using WowCharComparerWebApp.Configuration;
using WowCharComparerWebApp.Data.Database;

namespace WowCharComparerWebApp
{
    public class Startup
    {
        private string defaultConnectionString;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //services.AddLogging((logger) => { logger.AddConsole(); });


            defaultConnectionString = Configuration["ConnectionStrings:DefaultConnection"];
            APIConf.WoWCharacterComparerEmailPassword = Configuration["WowCharacterComparerEmailPassword"];
            APIConf.WowCharacterComparerEmail = Configuration["WowCharacterComparerEmail"];
            CheckDatabaseValidation(defaultConnectionString);
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


            // set configuration to logger 
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));

            // MVC configuration
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void CheckDatabaseValidation(string connectionString)
        {
            try
            {
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new Exception(InternalMessages.ConnectionStringIsEmpty);
                }

                using (DbContext dbContext = new ComparerDatabaseContext(connectionString))
                {
                    dbContext.Database.EnsureCreated();

                    if (dbContext.Database.IsSqlServer() == false)
                    {
                        throw new Exception(InternalMessages.InvalidDbType);
                    }
                    dbContext.Database.OpenConnection();
                    dbContext.Database.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
