using ABCBrasil.Hackthon.Api.Infra.Commons.Attributes;
using ABCBrasil.Hackthon.Api.Infra.Commons.Constants;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ABCBrasil.Hackthon.Api.Domain.DataContracts.Requests
{
    [ParamType(ApplicationConstants.TypeValidations.QUERY)]
    [SwaggerSchema("Payload para definir a paginação", Title = "PagingRequest")]
    public class PagingRequest
    {
        /// <summary>
        /// Página atual
        /// </summary>
        [FromQuery(Name = "page")]
        public int Page { get; set; } = 1;

        /// <summary>
        /// Itens por página
        /// </summary>
        [FromQuery(Name = "page_size")]
        public int PageSize { get; set; } = 10;
    }
}