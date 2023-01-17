using System;
using System.IO;
using System.Reflection;
using EShopping.Core.Domain;
using EShopping.Core.Persistence;
using EShoppingWebAPI.Filters;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;

namespace EShoppingWebAPI
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
            services.AddControllers(options =>
                options.Filters.Add(new AopExceptionHandlerFilter()));

            // Register the Swagger generator, defining 1 or more Swagger documents 
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "EShopping  WebAPI",
                    Description = "ASP.NET Core Web API",
                    TermsOfService = new Uri("https://www.linkedin.com/in/aman-toumaj-92114051/"),
                    Contact = new OpenApiContact
                    {
                        Name = "EShopping  Web API",
                        Email = string.Empty,
                        Url = new Uri("https://www.linkedin.com/in/aman-toumaj-92114051/"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Aman Toumaj",
                        Url = new Uri("https://www.linkedin.com/in/aman-toumaj-92114051/"),
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services
                .AddMvcCore()
                .AddApiExplorer()
                .AddFluentValidation(s =>
                {
                    s.RegisterValidatorsFromAssemblyContaining<Startup>();
                    //Omitted s.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                    s.AutomaticValidationEnabled = true;
                    s.ImplicitlyValidateChildProperties = true;
                });

            // Register the Swagger services
            //Omitted services.AddSwaggerDocument();

            services.AddDbContext<EShoppingDbContext>(opts => opts.UseSqlServer(Configuration["ConnectionStrings:EShoppingDB"]));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseStaticFiles();

            // Register the Swagger generator and the Swagger UI middlewares
            //Omitted app.UseOpenApi();

            //Omitted app.UseSwaggerUi3();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
