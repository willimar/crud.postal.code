using crud.api.core.repositories;
using data.provider.core;
using data.provider.core.mongo;
using graph.simplify.core;
using GraphQL.Server;
using GraphQL.Server.Internal;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using postal.code.api.Context;
using postal.code.api.GraphQL.Queries;
using postal.code.api.GraphQL.Types;
using postal.code.api.Mapper;
using postal.code.core.entities;
using postal.code.core.repositories;
using postal.code.core.services;
using System;
using System.Linq;
using System.Reflection;

namespace postal.code.api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Program.Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // If using Kestrel:
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddControllers();

            #region Assembly Info
            var assembly = GetType().Assembly;
            var assemblyInfo = assembly.GetName();

            var descriptionAttribute = assembly
                 .GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false)
                 .OfType<AssemblyDescriptionAttribute>()
                 .FirstOrDefault();
            var productAttribute = assembly
                 .GetCustomAttributes(typeof(AssemblyProductAttribute), false)
                 .OfType<AssemblyProductAttribute>()
                 .FirstOrDefault();
            var copyrightAttribute = assembly
                 .GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)
                 .OfType<AssemblyCopyrightAttribute>()
                 .FirstOrDefault();
            #endregion

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = productAttribute.Product,
                        Version = assemblyInfo.Version.ToString(),
                        Description = descriptionAttribute.Description,
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact
                        {
                            Name = copyrightAttribute.Copyright,
                            Url = new Uri(@"https://github.com/willimar/crud.postal.code"),
                            Email = "willimar in the gmail",
                        },
                        TermsOfService = null,
                        License = new Microsoft.OpenApi.Models.OpenApiLicense()
                        {
                            Name = "GNU General Public License v2.0",
                            Url = new Uri(@"https://github.com/willimar/crud.postal.code/blob/master/LICENSE")
                        }
                    });

            });

            services.AddCors(options => {
                options.AddPolicy(Program.AllowSpecificOrigins,
                    builder => {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            #region Dependences
            services.AddScoped<AddressService>();
            services.AddScoped<IRepository<Address>, AddressRepository>();
            services.AddScoped<IMongoClient, MongoClientFactory>();
            services.AddScoped<MapperProfile>();

            services.AddScoped<IDataProvider, DataProvider>(x => 
                new DataProvider(new MongoClientFactory(), Program.DataBaseName)
            );
            #endregion

            #region GraphQL Setup
            StartupResolve.ConfigureServices(services);
            services.AddScoped<IGraphQLExecuter<AppScheme<PostalCodeQuery>>, DefaultGraphQLExecuter<AppScheme<PostalCodeQuery>>>();
            services.AddScoped<PostalCodeQuery>();
            services.AddScoped<PostalCodeType>();
            services.AddScoped<CityType>();
            services.AddScoped<StateType>();
            services.AddScoped<CountryType>();
            services.AddScoped<GuidGraphType>();
            services.AddScoped<AppScheme<PostalCodeQuery>>();
            #endregion

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddOptions();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            #region Assembly Info
            var assembly = GetType().Assembly;
            var productAttribute = assembly
                 .GetCustomAttributes(typeof(AssemblyProductAttribute), false)
                 .OfType<AssemblyProductAttribute>()
                 .FirstOrDefault();
            var assemblyInfo = assembly.GetName();
            #endregion

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    $"{productAttribute.Product} v{assemblyInfo.Version}");

            });

            app.UseRouting();
            app.UseAuthorization();
            app.UseCors(Program.AllowSpecificOrigins);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            #region GraphQL Setup
            app.UseGraphQL<AppScheme<PostalCodeQuery>>();
            StartupResolve.Configure(app);
            #endregion
        }
    }
}
