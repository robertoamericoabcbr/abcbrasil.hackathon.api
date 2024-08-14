using ABCBrasil.Hackthon.Api.Infra.Swagger;
using Swashbuckle.AspNetCore.Annotations;

namespace ABCBrasil.Hackthon.Api.Domain.DataContracts.Responses
{
    [SwaggerSchema("Objeto de Detalhe do Erro", Title = "Error")]
    [SwaggerArrayValuesSchema("ParamType", ["body", "header", "query", "router"])]
    public class Error
    {
        /// <summary>
        /// Código do erro.
        /// </summary>
        /// <example>MISSING_INTENT_ID</example>
        [SwaggerSchema(Nullable = false)]
        public string Code { get; set; }

        /// <summary>
        /// Mensagem de erro.
        /// </summary>
        /// <example>O id da intenção é obrigatório.</example>
        [SwaggerSchema(Nullable = false)]
        public string Message { get; set; }

        /// <summary>
        /// Nome da parâmetro.
        /// </summary>
        /// <example>intentId</example>
        public string Param { get; set; }

        /// <summary>
        /// Origem do parâmetro.
        /// </summary>
        /// <example>router</example>
        public string ParamType { get; set; }

        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public Error(string code, string message, string param)
            : this(code, message)
        {
            Param = param;
        }

        public Error(string code, string message, string param, string paramType)
            : this(code, message, param)
        {
            ParamType = paramType;
        }
    }
}