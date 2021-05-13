using GraphQL.Server;
using GraphQLService.DAL;
using GraphQLService.Query.Implementations;
using GraphQLService.Query.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQLService.Logic;
using GraphQLService.Query;

namespace GraphQLService.Api
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
            services.AddScoped<IDbService, DbService>();

            services.AddScoped<ILabRepository, LabRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IListRepository, ListRepository>();
            services.AddScoped<ISeriesRepository, SeriesRepository>();
            services.AddScoped<IPointRepository, PointRepository>();

            services.AddScoped<AppSchema>();
            services.AddGraphQL()
                    .AddNewtonsoftJson()
                    .AddGraphTypes(typeof(AppSchema), ServiceLifetime.Scoped);

            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseGraphQL<AppSchema>();
            app.UseGraphQLPlayground();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
