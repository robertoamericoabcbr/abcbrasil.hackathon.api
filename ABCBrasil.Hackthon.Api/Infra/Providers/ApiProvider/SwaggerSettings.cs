using System;

using System.Collections.Generic;
using System.Linq;

namespace ABCBrasil.Hackthon.Api.Infra.Providers.ApiProvider
{
    public class SwaggerSettings
    {
        public bool? EnableAuthorizeSwaggerUI { get; set; }

        public string SwaggerPath { get; set; } = string.Empty;

        public List<SwaggerDocSettings> SwaggerDoc { get; set; }

        public void Validate()
        {
            if (SwaggerDoc == null || !SwaggerDoc.Any())
            {
                throw new InvalidOperationException("Não foi encontrada a seção ApiProvider:Swagger:SwaggerDoc nas configurações do sistema.");
            }

            foreach (SwaggerDocSettings item in SwaggerDoc)
            {
                if (string.IsNullOrWhiteSpace(item.ApplicationName))
                {
                    throw new InvalidOperationException("Não foi encontrada a seção ApiProvider:Swagger:SwaggerDoc:ApplicationName nas configurações do sistema.");
                }

                if (string.IsNullOrWhiteSpace(item.Version))
                {
                    throw new InvalidOperationException("Não foi encontrada a seção ApiProvider:Swagger:SwaggerDoc:Version nas configurações do sistema.");
                }
            }
        }
    }
}