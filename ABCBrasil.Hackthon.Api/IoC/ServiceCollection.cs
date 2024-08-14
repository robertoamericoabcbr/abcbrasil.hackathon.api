using ABCBrasil.Hackthon.Api.Domain.Interfaces.Repository;
using ABCBrasil.Hackthon.Api.Infra.Configurations;
using ABCBrasil.Hackthon.Api.Infra.Contexts;
using ABCBrasil.Hackthon.Api.Infra.Extensions;
using ABCBrasil.Hackthon.Api.Infra.Providers.ApiProvider;
using ABCBrasil.Hackthon.Api.Infra.Providers.CultureProvider;
using ABCBrasil.Hackthon.Api.Infra.Repository;
using ABCBrasil.Hackthon.Api.Infra.Swagger;
using ABCBrasil.Hackthon.Api.Infra.Swagger.Examples.Responses;
using ABCBrasil.Providers.BasicContractProvider.Lib;
using ABCBrasil.Providers.KeycloakProvider.Lib;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ABCBrasil.Hackthon.Api.IoC
{
    public static class ServiceCollection
    {
        /// <summary>
        /// AddApiProvider.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configuration"></param>
        /// <param name="serviceLifetime"></param>
        /// <returns></returns>
        public static IServiceCollection AddApiProvider(this IServiceCollection builder, IConfiguration configuration, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            var settings = Infra.Providers.ApiProvider.Settings.GetSettings(configuration);

            builder.AddSingleton(settings);
            builder.AddSwaggerExamplesFromAssemblyOf<ServiceUnavailableExample>();
            builder.AddSwaggerGen((c) =>
            {
                c.OperationFilter<SwaggerParameterDescriptionOperationFilter>();
                c.ParameterFilter<SwaggerParameterEnumParameterFilter>();
                c.ParameterFilter<SwaggerParameterArrayValuesParameterFilter>();
                c.ParameterFilter<SwaggerParameterSchemaParameterFilter>();
                c.SchemaFilter<SwaggerEnumSchemaFilter>();
                c.SchemaFilter<SwaggerArrayValuesSchemaFilter>();
                c.SchemaFilter<SwaggerDecorationSchemaFilter>();
                c.ExampleFilters();
                c.EnableAnnotations();

                ConfigureSwaggerDoc(settings, c);
            });

            builder.Configure((GzipCompressionProviderOptions options) =>
            {
                options.Level = CompressionLevel.Fastest;
            });

            builder.Configure((BrotliCompressionProviderOptions options) =>
            {
                options.Level = CompressionLevel.Fastest;
            });

            builder.Configure((ApiBehaviorOptions options) =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            builder.AddResponseCompression((options) =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                [
                    "application/json"
                ]);
                options.EnableForHttps = true;
            });

            return builder;
        }

        /// <summary>
        /// AddConfigureServices.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSections(configuration);
            services.AddServices();
            services.AddRepositories();
            services.AddApiVersioning();
            services.AddApiProvider(configuration, ServiceLifetime.Transient);
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddCors();
            services.AddControllersApi(configuration);
            services.AddCultureProvider(configuration);
            services.AddKeycloak(configuration);
            services.AddAutoMapper(typeof(Startup));
            services.AddMapper();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            return services;
        }

        /// <summary>
        /// AddKeycloak.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddKeycloak(this IServiceCollection builder, IConfiguration configuration)
        {
            builder.AddKeycloakAuthorize(configuration);
            builder.AddKeycloakAuthentication(configuration);

            return builder;
        }

        /// <summary>
        /// AddRepositories.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IServiceCollection AddRepositories(this IServiceCollection builder)
        {
            builder.AddScoped<IUserRepository, UserRepository>();

            return builder;
        }

        /// <summary>
        /// Configura o provedor de API.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseApiProvider(this IApplicationBuilder builder, IConfiguration configuration)
        {
            Infra.Providers.ApiProvider.Settings settings = Infra.Providers.ApiProvider.Settings.GetSettings(configuration);
            builder.UseResponseCompression();
            builder.UseSwagger();
            builder.UseSwaggerUI(delegate (SwaggerUIOptions c)
            {
                foreach (SwaggerDocSettings item in settings.Swagger.SwaggerDoc)
                {
                    c.SwaggerEndpoint($"/swagger/{item.Version}/swagger.json", item.ApplicationName + " - " + item.Version);
                    c.RoutePrefix = settings.Swagger.SwaggerPath;
                }
            });

            return builder;
        }

        /// <summary>
        /// UseConfigure.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseConfigure(this IApplicationBuilder builder, IConfiguration configuration)
        {
            builder.UseApiProvider(configuration);
            builder.UseRouting();
            builder.UseCors(p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            builder.UseEndpoints(config => config.MapDefaultControllerRoute());
            return builder;
        }

        /// <summary>
        /// AddControllersApi.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private static IServiceCollection AddControllersApi(this IServiceCollection builder, IConfiguration configuration)
        {
            builder
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.Converters.Add(new DateJsonConverter(configuration));
                    options.JsonSerializerOptions.Converters.Add(new TimeSpanConverter());
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });

            return builder;
        }

        /// <summary>
        /// AddMapper.
        /// </summary>
        /// <param name="builder"></param>
        private static IServiceCollection AddMapper(this IServiceCollection builder)
        {
            builder.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return builder;
        }

        /// <summary>
        /// SetSections.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private static void AddSections(this IServiceCollection builder, IConfiguration configuration)
        {
            builder.Configure<ApiConfig>(configuration.GetSection("Api"));
        }

        /// <summary>
        /// AddService.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        private static IServiceCollection AddServices(this IServiceCollection builder)
        {
            builder.AddScoped(provider => provider.GetService<INotificationProvider>().CreateNotification());

            builder.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            return builder;
        }

        ///<summary>
        /// ConfigureSwaggerDoc.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="c"></param>
        private static void ConfigureSwaggerDoc(Infra.Providers.ApiProvider.Settings settings, SwaggerGenOptions c)
        {
            foreach (SwaggerDocSettings item in settings.Swagger.SwaggerDoc)
            {
                c.CustomSchemaIds(x => x.FullName.Replace("+", "."));

                c.SwaggerDoc(item.Version, new OpenApiInfo
                {
                    Version = item.Version,
                    Title = item.ApplicationName + " - " + item.Version,
                    Description = File.Exists("README.md") ? File.ReadAllText("README.md") ?? "" : ""
                });

                if (!string.IsNullOrWhiteSpace(item.AssemblyXmlName))
                {
                    string text = Path.Combine(AppContext.BaseDirectory, item.AssemblyXmlName ?? "");

                    if (File.Exists(text))
                    {
                        c.IncludeXmlComments(text, includeControllerXmlComments: true);
                    }
                }
            }
        }
    }
}