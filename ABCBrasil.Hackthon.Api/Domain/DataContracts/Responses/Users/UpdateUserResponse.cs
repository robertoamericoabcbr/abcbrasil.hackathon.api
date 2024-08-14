using ABCBrasil.Hackthon.Api.Infra.Commons;
using ABCBrasil.Hackthon.Api.Infra.Swagger;
using Swashbuckle.AspNetCore.Annotations;
using System;

namespace ABCBrasil.Hackthon.Api.Domain.DataContracts.Responses.Users
{
    [SwaggerSchema("Resposta da requisição da atualização do usuário", Title = "UpdateUserResponse")]
    public class UpdateUserResponse
    {
        /// <summary>
        /// Identificador do usuário.
        /// </summary>
        /// <example>b1f56932-073e-4e5c-a358-cc36b0967697</example>
        [SwaggerSchemaDecoration(Nullable = false)]
        public string Id { get; set; }

        /// <summary>
        /// Nome do usuário.
        /// </summary>
        /// <example>Roberto Americo</example>
        [SwaggerSchemaDecoration(Nullable = false)]
        public string Name { get; set; }

        /// <summary>
        /// E-mail do usuário.
        /// </summary>
        /// <example>roberto.americo@abcbrasil.com</example>
        [SwaggerSchemaDecoration(Nullable = false, Pattern = SwaggerPatternsConstants.EMAIL)]
        public string Email { get; set; }

        /// <summary>
        /// Senha do usuário.
        /// </summary>
        /// <example>abc@2024</example>
        [SwaggerSchemaDecoration(Nullable = false)]
        public string Password { get; set; }

        /// <summary>
        /// Data da criação do usuário.
        /// </summary>
        /// <example>2023-10-27T11:46:46.1703588-03:00</example>
        [SwaggerSchemaDecoration(Nullable = false, Pattern = SwaggerPatternsConstants.DATE_TIME_ZONE)]
        public DateTimeOffset CreatedAt { get; set; }
    }
}