using EventsPlanner.DomainContext.DataContext;
using EventsPlanner.DomainContext.DomainEntities;
using EventsPlanner.DomainContext.Repository;
using EventsPlanner.WebAPI.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsPlanner.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string CorsSettingOrigin = "ReactRequestOrigin";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DomainDataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("EventPlannerConnection")));

            services.AddControllers();

            IoC.AddDependency(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EventsPlanner.WebAPI", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: CorsSettingOrigin,
                    builder =>
                    {
                        builder.WithOrigins("*").AllowAnyHeader().SetIsOriginAllowedToAllowWildcardSubdomains().AllowAnyMethod();
                    });
            });
            }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DomainDataContext domainDataContext)
        {
            UpdateDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EventsPlanner.WebAPI v1"));
            }


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(CorsSettingOrigin);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<DomainDataContext>())
                {
                    context.Database.EnsureDeleted();

                    context.Database.Migrate();
                    
                    context.Database.EnsureCreated();

                    RelationalDatabaseCreator databaseCreator = (RelationalDatabaseCreator)context.Database.GetService<IDatabaseCreator>();
                    databaseCreator.CreateTables();

                    if (!context.Role.Any())
                    {
                        context.Role.AddRange(new Role
                        {
                            RoleId = "Admin",
                            RoleDescription = "Role Admin"
                        },
                        new Role
                        {
                            RoleId = "User",
                            RoleDescription = "Role User"
                        },
                        new Role
                        {
                            RoleId = "Public",
                            RoleDescription = "Role Public"
                        }
                        );
                        context.SaveChanges();
                    }

                    if (!context.User.Any())
                    {
                        context.User.AddRange(new User
                        {
                            UserLogin = "admin",
                            Name = "Admin",
                            Lastname = "Admin",
                            Password = "admin1234",
                            RoleId = "Admin",
                            Active = true,
                            Email = ""
                        },
                        new User
                        {
                            UserLogin = "user",
                            Name = "User",
                            Lastname = "User",
                            Password = "user1234",
                            RoleId = "User",
                            Active = true,
                            Email = ""
                        }
                        );

                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
