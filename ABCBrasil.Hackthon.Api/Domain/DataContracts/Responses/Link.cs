using ABCBrasil.Hackthon.Api.Infra.Swagger;
using Swashbuckle.AspNetCore.Annotations;

namespace ABCBrasil.Hackthon.Api.Domain.DataContracts.Responses
{
    [SwaggerSchema("Dados sobre o link", Title = "Link")]
    [SwaggerArrayValuesSchema("Type", ["GET"])]
    [SwaggerArrayValuesSchema("Rel", ["self", "prev", "next", "first", "last"])]
    public class Link
    {
        /// <summary>
        /// Tipo da ação.
        /// </summary>
        /// <example>GET</example>
        [SwaggerSchema(Nullable = false)]
        public string Type { get; set; }

        /// <summary>
        /// Tipo da paginação.
        /// </summary>
        /// <example>self</example>
        [SwaggerSchema(Nullable = false)]
        public string Rel { get; set; }

        /// <summary>
        /// Path do endpoint.
        /// </summary>
        /// <example>/financial-institutions</example>
        [SwaggerSchema(Nullable = false)]
        public string Uri { get; set; }

        internal static Link Self(string version, string id)
        {
            return new Link
            {
                Type = "GET",
                Rel = "self",
                Uri = $"/api/v{version}/intents/{id}"
            };
        }
    }
}