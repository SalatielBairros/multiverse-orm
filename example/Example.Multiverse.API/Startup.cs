using Dapper.Fluent.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Dapper.Fluent.Repository.Impl;
using Multiverse.Extensions;
using Multiverse.Contracts;
using Multiverse.Postgres.Extensions;
using Dapper.Fluent.Repository.Mappers;
using Multiverse.MultiSchema;
using Dapper.Fluent.Repository.Migration;

namespace Dapper.Fluent.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            
            services                
                .AddPostgresRepositoryWithMigration(
                    Configuration["ConnectionString"],
                    assembliesWithMappers: typeof(Reference).Assembly)
                .AddMapperConfiguration<MapperConfiguration>()
                .AddDapperORM()
                .AddHttpMultiSchema();            

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dapper.Fluent.API", Version = "v1" });
            });
            
            services.AddScoped<IDapperFluentService, DapperFluentService>();
            services.AddScoped<IPublicSchemaEntityRepository, PublicSchemaEntityRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IBigDataRepository, BigDataRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDapperORMRunner dapper)
        {
            dapper.AddMappers();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dapper.Fluent.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
