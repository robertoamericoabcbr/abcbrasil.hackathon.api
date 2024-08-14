using ABCBrasil.Hackthon.Api.Domain.DataContracts.Responses;
using ABCBrasil.Hackthon.Api.Domain.DataContracts.Responses.Users;
using ABCBrasil.Providers.BasicContractProvider.Lib;
using ABCBrasil.Providers.CommonProvider.Lib;
using MediatR;
using System.Collections.Generic;

namespace ABCBrasil.Hackthon.Api.Domain.CommandQuerySeparation.Commands
{
    public class UpdateUserCommandRequest : IRequest<Try<List<NotificationBase>, ApiResponse<UpdateUserResponse>>>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}