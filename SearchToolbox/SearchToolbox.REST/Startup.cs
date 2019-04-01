using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using SearchToolbox.Interfaces;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;

namespace SearchToolbox.REST
{
    /// <summary>
    /// Startup class for the web apu
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configuration property for the web api
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Startup method for the web api
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration method for the web api. This method gets called by the runtime, and adds services to the container
        /// </summary>
        /// <param name="serviceCollection"></param>
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("AppSettings.json", false, true)
                .Build();

            serviceCollection.AddSingleton(new LoggerFactory()
                .AddSerilog()
                );

            serviceCollection.AddLogging();

            serviceCollection.AddOptions();

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Async(
                    a => a.File(configuration["Configuration:Logging:LogFilePath"] + string.Format(configuration["Configuration:Logging:LogFileName"], DateTime.Now)),
                    bufferSize: 10000)
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .CreateLogger();

            AppSettings appSettings = Configuration.GetSection("Configuration").Get<AppSettings>();
            serviceCollection.AddSingleton(appSettings);

            serviceCollection.AddSingleton<IDataAccessLayer, DAL.Utilities>();
            serviceCollection.AddSingleton<IBusinessLogicLayer, BLL.Utilities>();

            #region Swagger
            serviceCollection.AddSwaggerGen(swaggerOptions =>
            {
                swaggerOptions.SwaggerDoc("v1", new Info { Title = appSettings.DisplayName, Version = "v1" });

                string xmlDocumentationFilePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, $"{appSettings.Name}.xml");
                swaggerOptions.IncludeXmlComments(xmlDocumentationFilePath);
            });
            #endregion

            serviceCollection.AddMvc();
        }

        /// <summary>
        /// The configure method for the web api. This method gets called by the runtime, and is used to configure the HTTP request pipeline
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            AppSettings appSettings = app.ApplicationServices.GetService<AppSettings>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{appSettings.DisplayName} v1");
                c.RoutePrefix = string.Empty;
            });
            #endregion

            app.UseMiddleware(typeof(GenericExceptionHandler));

            app.UseMvc();
        }
    }
}
