using ABCBrasil.Hackthon.Api.Domain.DataContracts.Responses;
using ABCBrasil.Hackthon.Api.Domain.Interfaces.Repository;
using ABCBrasil.Hackthon.Api.Infra.Extensions;
using ABCBrasil.Providers.BasicContractProvider.Lib;
using ABCBrasil.Providers.CommonProvider.Lib;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ABCBrasil.Hackthon.Api.Domain.CommandQuerySeparation.Queries
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommandQuery, Try<List<NotificationBase>, ApiResponse<bool>>>
    {
        private readonly IMapper _mapper;
        private readonly INotificationManager _notificationManager;
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(
            INotificationManager notificationManager,
            IMapper mapper,
            IUserRepository userRepository)
        {
            _notificationManager = notificationManager;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<Try<List<NotificationBase>, ApiResponse<bool>>> Handle(DeleteUserCommandQuery commandQuery, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(commandQuery.Id);

                if (user == null)
                {
                    _notificationManager.AddNotFound();

                    return _notificationManager.GetNotifications();
                }

                await _userRepository.DeleteAsync(user);

                return new ApiResponse<bool>(true);
            }
            catch
            {
                _notificationManager.AddServiceUnavailable();

                return _notificationManager.GetNotifications();
            }
        }
    }
}