using Microsoft.Extensions.Configuration;
using System;

namespace ABCBrasil.Hackthon.Api.Infra.Providers.CultureProvider
{
    public class Settings
    {
        public string LanguageCode { get; set; }

        public string ShortDatePattern { get; set; }

        public string DateSeparator { get; set; }

        public static Settings GetSettings(IConfiguration configuration)
        {
            Settings settings = new Settings();
            (configuration?.GetSection("CultureProvider") ?? throw new InvalidOperationException("Não foi encontrada a seção CultureProvider nas configurações do sistema.")).Bind(settings);
            if (string.IsNullOrWhiteSpace(settings.LanguageCode))
            {
                throw new InvalidOperationException("Não foi encontrada a seção CultureProvider:LanguageCode nas configurações do sistema.");
            }

            if (string.IsNullOrWhiteSpace(settings.ShortDatePattern))
            {
                throw new InvalidOperationException("Não foi encontrada a seção CultureProvider:ShortDatePattern nas configurações do sistema.");
            }

            if (string.IsNullOrWhiteSpace(settings.DateSeparator))
            {
                throw new InvalidOperationException("Não foi encontrada a seção CultureProvider:DateSeparator nas configurações do sistema.");
            }

            return settings;
        }
    }
}