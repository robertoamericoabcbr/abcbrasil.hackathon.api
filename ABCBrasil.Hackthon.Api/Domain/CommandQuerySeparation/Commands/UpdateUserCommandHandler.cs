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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABCBrasil.Hackthon.Api.Domain.CommandQuerySeparation.Commands
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommandRequest, Try<List<NotificationBase>, ApiResponse<UpdateUserResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly INotificationManager _notificationManager;
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(
            INotificationManager notificationManager,
            IMapper mapper,
            IUserRepository userRepository)
        {
            _notificationManager = notificationManager;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<Try<List<NotificationBase>, ApiResponse<UpdateUserResponse>>> Handle(UpdateUserCommandRequest command, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(command.Id);

                user.Name = command.Name;
                user.Email = command.Email;
                user.Password = command.Password;

                await _userRepository.UpdateAsync(user);

                var updateUserResponse = _mapper.Map<UpdateUserResponse>(user);

                return new ApiResponse<UpdateUserResponse>(updateUserResponse);
            }
            catch
            {
                _notificationManager.AddServiceUnavailable();

                return _notificationManager.GetNotifications();
            }
        }
    }
}