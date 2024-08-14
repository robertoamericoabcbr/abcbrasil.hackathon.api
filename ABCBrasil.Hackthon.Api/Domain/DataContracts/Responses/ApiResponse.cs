using ABCBrasil.Hackthon.Api.Infra.Swagger;
using System.ComponentModel.DataAnnotations;

namespace ABCBrasil.Hackthon.Api.Domain.DataContracts.Responses
{
    [SwaggerSchemaDecoration(Title = "ApiResponse")]
    public class ApiResponse<TData>
    {
        [Required]
        public TData Data { get; set; }

        public ApiResponse(TData data)
        {
            Data = data;
        }
    }
}