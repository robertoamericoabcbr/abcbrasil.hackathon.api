using ABCBrasil.Hackthon.Api.Domain.DataContracts.Responses;
using ABCBrasil.Hackthon.Api.Domain.DataContracts.Responses.Users;
using ABCBrasil.Hackthon.Api.Domain.Interfaces.Repository;
using ABCBrasil.Hackthon.Api.Domain.Models;
using ABCBrasil.Hackthon.Api.Infra.Extensions;
using ABCBrasil.Providers.BasicContractProvider.Lib;
using ABCBrasil.Providers.CommonProvider.Lib;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ABCBrasil.Hackthon.Api.Domain.CommandQuerySeparation.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, Try<List<NotificationBase>, ApiResponse<CreateUserResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly INotificationManager _notificationManager;
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(
            INotificationManager notificationManager,
            IMapper mapper,
            IUserRepository userRepository)
        {
            _notificationManager = notificationManager;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<Try<List<NotificationBase>, ApiResponse<CreateUserResponse>>> Handle(CreateUserCommandRequest command, CancellationToken cancellationToken)
        {
            try
            {
                var user = _mapper.Map<User>(command);

                await _userRepository.AddAsync(user);

                var createUserResponse = _mapper.Map<CreateUserResponse>(user);

                return new ApiResponse<CreateUserResponse>(createUserResponse);
            }
            catch
            {
                _notificationManager.AddServiceUnavailable();

                return _notificationManager.GetNotifications();
            }
        }
    }
}