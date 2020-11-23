using System.Net.Http;
using MatBlazor;
using AssetManagement.Core;
using AssetManagement.DataAccessLibrary.DataModels;
using AssetManagement.DataAccessLibrary.DbContexts;
using AssetManagement.DataAccessLibrary.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AssetManagement.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Core.AssetController.StartWatchingAlienData();
            ISqlDataAccess<AssetRecordData> assetRecordDbAccess = new SqlDataAccess<AssetRecordData>(new AssetRecordContext());
            new AssetRecordManager(assetRecordDbAccess).StartWatchingForAssetStatusChange();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Temporary SQL data access example - commented out due to connection string not being part of appsettings.json
            // DataAccessLibrary.SqlDataAccess sqlDataAccessor = new DataAccessLibrary.SqlDataAccess();
            // sqlDataAccessor.SampleDatabaseOperations(); 

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<AssetService>();
            services.AddMatBlazor();
            services.AddScoped<HttpClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
