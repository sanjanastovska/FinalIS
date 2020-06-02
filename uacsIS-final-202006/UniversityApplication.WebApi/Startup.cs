using AutoMapper;

using BankApplication.Data;
using BankApplication.Service.Repositories;
using BankApplication.Service.Service;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.Swagger;
using UniversityApplication.WebApi.Infrastructure;

namespace BankApplication.WebApi
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
            services.AddControllersWithViews();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddMaps("BankApplication.Data");
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services
                .Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));
            services
                .AddSingleton<IConfiguration>(Configuration);
            services
                .AddDbContextPool<BankDataContext>((serviceProvider, options) =>
                {
                    options
                        .UseSqlServer(Configuration.GetSection("ConnectionStrings")
                                .GetSection("DefaultConnection").Value,
                            x =>
                            {
                                x.MigrationsAssembly("BankApplication.Data");
                                x.CommandTimeout(60);
                            });
                });

            services
                .AddScoped<IClientsRepository, ClientService>()
                .AddScoped<IAccountsRepository, AccountsService>();

            //Swagger
            //services.AddSwaggerGen(x =>
            //{
            //    x.SwaggerDoc("v1", new Info {Title = "uacsWCFapplication", Version = "v1"});

            //});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {            
            app.EnsureMigrationOfContext<BankDataContext>(); /* Automatic Migrations Version 2*/
            //UpdateDatabase(app); /* Automatic Migrations Version 2*/

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //Swagger
            //var swaggerOptions = new SwaggerOptions();
            //Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

            //app.UseSwagger(option =>
            //{
            //    option.RouteTemplate = swaggerOptions.JsonRoute;
            //});

            //app.UseSwagger(option =>
            //{
            //    option.SwaggerEndpoint(swaggerOptions.UiEndpoints, swaggerOptions.Description);
            //});




            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }


        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();

            using var context = serviceScope.ServiceProvider.GetService<BankDataContext>();
            context.Database.Migrate();
        }
    }
}
