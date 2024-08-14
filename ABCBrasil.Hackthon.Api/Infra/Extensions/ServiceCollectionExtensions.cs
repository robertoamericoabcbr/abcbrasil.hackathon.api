using ABCBrasil.Hackthon.Api.Infra.Providers.CultureProvider;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System.Globalization;

namespace ABCBrasil.Hackthon.Api.Infra.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCultureProvider(this IServiceCollection builder, IConfiguration configuration)
        {
            CultureInfo.CurrentCulture = (CultureInfo.DefaultThreadCurrentUICulture = (CultureInfo.DefaultThreadCurrentCulture = (CultureInfo.CurrentUICulture = CultureGlobal.GetCultureInfo(Infra.Providers.CultureProvider.Settings.GetSettings(configuration)))));
            return builder;
        }
    }
}