using System;
using System.Diagnostics.CodeAnalysis;

using FluentValidation;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

using NLSL.SKS.Package.BusinessLogic;
using NLSL.SKS.Package.BusinessLogic.Interfaces;
using NLSL.SKS.Package.BusinessLogic.Validators;
using NLSL.SKS.Package.DataAccess.Interfaces;
using NLSL.SKS.Package.DataAccess.Sql;
using NLSL.SKS.Package.ServiceAgents;
using NLSL.SKS.Package.ServiceAgents.Interface;
using NLSL.SKS.Package.Services.Filter;
using NLSL.SKS.Package.WebhookManager.Interfaces;

namespace NLSL.SKS.Package.Services
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private readonly IWebHostEnvironment _hostingEnv;

        public IConfiguration Configuration
        {
            get;
        }

        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            Configuration = configuration;
            _hostingEnv = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddHttpClient();
            services.AddAutoMapper(typeof(Startup));

            services.AddValidatorsFromAssemblyContaining<ParcelValidator>(ServiceLifetime.Transient);

            services.AddTransient<IParcelLogic, ParcelLogic>();
            services.AddTransient<IWarehouseLogic, WarehouseLogic>();
            services.AddTransient<IWarehouseRepository, WarehouseRepository>();
            services.AddTransient<IParcelRepository, ParcelRepository>();
            services.AddTransient<IGeoCodingAgent, GeoCodingAgent>();
            services.AddTransient<IHttpAgent, HttpAgent>();
            services.AddTransient<IWebHookLogic, WebHookLogic>();
            services.AddTransient<IWebHookRepository, WebHookRepository>();
            services.AddTransient<IWebHookManager, WebhookManager.WebhookManager>();


            string connectionString = Configuration.GetConnectionString("Database");
            services.AddDbContext<PackageContext>(options =>
                                                      options
                                                          .UseLazyLoadingProxies()
                                                          .UseSqlServer(connectionString,
                                                              x => x.UseNetTopologySuite()));


            services
                .AddMvc(options =>
                        {
                            options.InputFormatters.RemoveType<SystemTextJsonInputFormatter>();
                            options.OutputFormatters.RemoveType<SystemTextJsonOutputFormatter>();
                        })
                .AddNewtonsoftJson(opts =>
                                   {
                                       opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                                       opts.SerializerSettings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));
                                   })
                .AddXmlSerializerFormatters();


            services
                .AddSwaggerGen(c =>
                               {
                                   c.SwaggerDoc("1.20.1", new OpenApiInfo
                                                          {
                                                              Version = "1.20.1",
                                                              Title = "Parcel Logistics Service",
                                                              Description = "Parcel Logistics Service (ASP.NET 5)",
                                                              Contact = new OpenApiContact
                                                                        {
                                                                            Name = "SKS",
                                                                            Url = new Uri("https://www.technikum-wien.at/"),
                                                                            Email = ""
                                                                        }
                                                          });
                                   c.CustomSchemaIds(type => type.FullName);
                                   //c.IncludeXmlComments($"{AppContext.BaseDirectory}{Path.DirectorySeparatorChar}{_hostingEnv.ApplicationName}.xml");

                                   // Include DataAnnotation attributes on Controller Action parameters as Swagger validation rules (e.g required, pattern, ..)
                                   // Use [ValidateModelState] on Actions to actually validate it in C# as well!
                                   c.OperationFilter<GeneratePathParamsValidationFilter>();
                               });

            services.AddSwaggerGen(c =>
                                   {
                                       c.SwaggerDoc("v1", new OpenApiInfo { Title = "NLSL.SKS.Package.Services", Version = "v1" });
                                   });


            services.AddCors();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NLSL.SKS.Package.Services v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
                             {
                                 //TODO: Either use the SwaggerGen generated Swagger contract (generated from C# classes)
                                 c.SwaggerEndpoint("/swagger/1.20.1/swagger.json", "Parcel Logistics Service");

                                 //TODO: Or alternatively use the original Swagger contract that's included in the static files
                                 // c.SwaggerEndpoint("/swagger-original.json", "Parcel Logistics Service Original");
                             });

            //TODO: Use Https Redirection
            // app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
                             {
                                 endpoints.MapControllers();
                             });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //TODO: Enable production exception handling (https://docs.microsoft.com/en-us/aspnet/core/fundamentals/error-handling)
                app.UseExceptionHandler("/Error");

                app.UseHsts();
            }


            using (IServiceScope serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                PackageContext context = serviceScope.ServiceProvider.GetRequiredService<PackageContext>();
                context.Database.EnsureCreated();

            }
        }
    }
}