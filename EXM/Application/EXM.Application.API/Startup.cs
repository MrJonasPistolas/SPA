using EXM.Application.API.Extensions;
using EXM.Application.API.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace EXM.Application.API
{
    /// <summary>
    /// Represents the startup process for the application
    /// </summary>
    public class Startup
    {
        private readonly string CORSOpenPolicy = "OpenCORSPolicy";
        /// <summary>
        /// Gets the current configuration.
        /// </summary>
        /// <value>The current application configuration.</value>
        public IConfiguration Configuration { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The current configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configures services for the application.
        /// </summary>
        /// <param name="services">The collection of services to configure the application with.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: CORSOpenPolicy, builder => { builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod(); });
            });
            services.AddCurrentUserService();
            services.AddSerialization();
            services.AddDatabase(Configuration);
            services.AddIdentity();
            services.AddJwtAuthentication(services.GetApplicationSettings(Configuration));
            services.AddApplicationServices(Configuration);
            //services.AddRepositories();
            services.RegisterSwagger();
            services.AddControllers();
            services.AddRazorPages();
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(CORSOpenPolicy);
            app.UseExceptionHandling(env);
            app.UseHttpsRedirection();
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Files")),
                RequestPath = new PathString("/Files")
            });
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints();
            app.ConfigureSwagger();
            app.Initialize();
        }
    }
}
