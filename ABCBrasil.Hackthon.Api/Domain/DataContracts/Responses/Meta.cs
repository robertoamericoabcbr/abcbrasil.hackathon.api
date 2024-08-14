using ABCBrasil.Hackthon.Api.Infra.Commons;
using ABCBrasil.Hackthon.Api.Infra.Swagger;
using Swashbuckle.AspNetCore.Annotations;
using System;

namespace ABCBrasil.Hackthon.Api.Domain.DataContracts.Responses
{
    [SwaggerSchema("Meta dados", Title = "Meta")]
    public class Meta
    {
        /// <summary>
        /// Total de itens.
        /// </summary>
        /// <example>50</example>
        public long TotalRecords { get; set; }

        /// <summary>
        /// Data da requisição.
        /// </summary>
        /// <example>2023-10-27T11:46:46.1291249-03:00</example>
        [SwaggerSchemaDecoration(Pattern = SwaggerPatternsConstants.DATE_TIME_ZONE)]
        public DateTimeOffset RequestDateTime { get; set; }

        /// <summary>
        /// Id da correlação
        /// </summary>
        public string CorrelationId { get; set; }
    }
}