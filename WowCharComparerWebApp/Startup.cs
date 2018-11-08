using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            defaultConnectionString = Configuration["ConnectionStrings:DefaultConnection"];
            InitializeDatabaseConnection(defaultConnectionString);
            TemporarySolutions.Request();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

        private void InitializeDatabaseConnection(string connectionString)
        {
            bool correctDbType = default(bool);

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception(InternalMessages.ConnectionStringIsEmpty);
            }

            using (DbContext dbContext = new ComparerDatabaseContext(connectionString))
            {
                correctDbType = dbContext.Database.IsSqlServer();
                dbContext.Database.EnsureCreated();
            }

            if (correctDbType == false)
            {
                throw new Exception(InternalMessages.InvalidDbType);
            }
        }
    }
}
