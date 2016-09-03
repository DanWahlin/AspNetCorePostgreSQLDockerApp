using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AspNetCorePostgreSQLDockerApp.Repository;
using System.IO;
using Microsoft.Extensions.FileProviders;

namespace AspNetCorePostgreSQLDockerApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //Add PostgreSQL support
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<DockerCommandsDbContext>(options =>
                    options.UseNpgsql(Configuration["Data:DbContext:DockerCommandsConnectionString"]))
                .AddDbContext<CustomersDbContext>(options =>
                    options.UseNpgsql(Configuration["Data:DbContext:CustomersConnectionString"]));


            services.AddMvc();

            // Add our PostgreSQL Repositories
            services.AddTransient<IDockerCommandsRepository, DockerCommandsRepository>();
            services.AddTransient<ICustomersRepository, CustomersRepository>();
            services.AddTransient<DockerCommandsDbSeeder>();
            services.AddTransient<CustomersDbSeeder>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
                              DockerCommandsDbSeeder dockerCommandsDbSeeder, CustomersDbSeeder customersDbSeeder)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //app.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());
 
            // Route all unknown requests to app root
            app.Use(async (context, next) =>
            {
                await next();

                // Handle Angular routes which won't work on the server-side
                if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
                {
                    context.Request.Path = "/index.html";  
                    await next();
                }
            });

            // Serve wwwroot as root
            app.UseFileServer();

            // Serve /node_modules as a separate root (for packages that use other npm modules client side)
            app.UseFileServer(new FileServerOptions()
            {
                // Set root of file server
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "node_modules")),
                // Only react to requests that match this path
                RequestPath = "/node_modules", 
                // Don't expose file system
                EnableDirectoryBrowsing = false
            });

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            customersDbSeeder.SeedAsync(app.ApplicationServices).Wait();
            dockerCommandsDbSeeder.SeedAsync(app.ApplicationServices).Wait();

        }

    }
}
