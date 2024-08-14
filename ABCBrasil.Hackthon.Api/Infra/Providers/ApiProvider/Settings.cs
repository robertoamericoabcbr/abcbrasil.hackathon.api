using Microsoft.Extensions.Configuration;
using System;

namespace ABCBrasil.Hackthon.Api.Infra.Providers.ApiProvider
{
    public class Settings
    {
        public SwaggerSettings Swagger { get; set; }

        public static Settings GetSettings(IConfiguration configuration)
        {
            Settings settings = CreateSettings(configuration);
            settings.Validate();
            return settings;
        }

        private static Settings CreateSettings(IConfiguration configuration)
        {
            IConfigurationSection configuration2 = configuration?.GetSection("ApiProvider");
            Settings settings = new Settings();
            configuration2.Bind(settings);
            return settings;
        }

        private void Validate()
        {
            if (Swagger == null)
            {
                throw new InvalidOperationException("Não foi encontrada a seção ApiProvider nas configurações do sistema.");
            }

            ValidateSwagger();
        }

        private void ValidateSwagger()
        {
            if (Swagger == null)
            {
                throw new InvalidOperationException("Não foi encontrada a seção ApiProvider:Swagger nas configurações do sistema.");
            }

            Swagger.Validate();
        }
    }
}