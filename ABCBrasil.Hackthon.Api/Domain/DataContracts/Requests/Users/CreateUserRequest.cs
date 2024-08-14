using ABCBrasil.Hackthon.Api.Infra.Commons;
using ABCBrasil.Hackthon.Api.Infra.Commons.Attributes;
using ABCBrasil.Hackthon.Api.Infra.Commons.Constants;
using ABCBrasil.Hackthon.Api.Infra.Swagger;
using Swashbuckle.AspNetCore.Annotations;

namespace ABCBrasil.Hackthon.Api.Domain.DataContracts.Requests.Users
{
    [ParamType(ApplicationConstants.TypeValidations.BODY)]
    [SwaggerSchema("Payload para criar um usuário", Title = "CreateUserRequest", Required = ["Name", "Email", "Password"])]
    public class CreateUserRequest
    {
        /// <summary>
        /// Nome do Usuário.
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
    }
}