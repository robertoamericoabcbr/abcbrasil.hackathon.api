using ABCBrasil.Hackthon.Api.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ABCBrasil.Hackthon.Api
{
    /// <summary>
    /// Startup.
    /// </summary>
    /// <author>
    /// Arquitetura - Engenharia - Banco ABCBrasil - 2021
    /// </author>
    public class Startup
    {
        /// <summary>
        /// Configuration.
        /// </summary>
        /// <author>
        /// Arquitetura - Engenharia - Banco ABCBrasil - 2021
        /// </author>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Startup.
        /// </summary>
        /// <param name="configuration"></param>
        /// <author>
        /// Arquitetura - Engenharia - Banco ABCBrasil - 2021
        /// </author>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// ConfigureServices.
        /// </summary>
        /// <param name="services"></param>
        /// <author>
        /// Arquitetura - Engenharia - Banco ABCBrasil - 2021
        /// </author>
        public void ConfigureServices(IServiceCollection services)
            => services.AddConfigureServices(Configuration);

        /// <summary>
        /// Configure.
        /// </summary>
        /// <param name="app"></param>
        /// <author>
        /// Arquitetura - Engenharia - Banco ABCBrasil - 2021
        /// </author>
        public void Configure(IApplicationBuilder app)
            => app.UseConfigure(Configuration);
    }
}