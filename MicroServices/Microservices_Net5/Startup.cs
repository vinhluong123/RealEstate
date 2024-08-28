using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microservices_Net5.Repository;
using Microservices_Net5.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Serilog;
using System;
using System.IO;
using System.Reflection;

namespace Microservices_Net5
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
            // Start Swagger for API routing
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Microservices_Net5",
                    Version = "v1",
                    Description = "This is the API for development testing only",
                    TermsOfService = new Uri("https://www.example.com/terms"), // Use a valid URL
                    Contact = new OpenApiContact
                    {
                        Name = "Vinh Luong",
                        Email = "abc@gmail.com",
                        Url = new Uri("https://www.example.com/contact") // Use a valid URL
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://www.example.com/license") // Optional: Add if you have a license URL
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            // For Entity Framework
            services.AddDbContext<SchoolContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

           
            JwtBearerConfiguration.Register(services, Configuration);
            services.AddControllersWithViews();

            // Add repository here
            services.AddScoped<IStudentRepository, StudentRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();

                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Microservices_Net5 v1"));
            }

            app.UseHttpsRedirection();

            //Add support to logging request with SERILOG
            //app.UseSerilogRequestLogging();

            app.UseRouting();

            //when a request will come to server, It will find token & will try to validate it.
            //If token is valid, It will set User.Identity.IsAuthenticated to true and it will also set claims in 'User.Identity'.
            // Important: the order of Authen and Authorization
            app.UseAuthentication();
            app.UseAuthorization();
            
            ///////-----------------///////////

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
