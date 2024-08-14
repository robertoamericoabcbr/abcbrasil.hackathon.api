using ABCBrasil.Hackthon.Api.Domain.CommandQuerySeparation.Commands;
using ABCBrasil.Hackthon.Api.Domain.DataContracts.Requests.Users;
using ABCBrasil.Hackthon.Api.Domain.DataContracts.Responses.Users;
using ABCBrasil.Hackthon.Api.Domain.Models;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;

namespace ABCBrasil.Hackthon.Api.Domain.Mappings
{
    public class MappingProfile : Profile
    {
        #region Props

        private static IConfiguration _config;

        public static IConfiguration Config
        {
            get => _config;
            private set => _config = value;
        }

        public static void SetConfig(IConfiguration config)
        {
            Config = config;
            if (Mapper == null)
            {
                _ = new MappingProfile();
            }
        }

        private static IMapper _mapper;

        public static IMapper Mapper
        {
            get => _mapper;
            private set => _mapper = value;
        }

        #endregion Props

        public MappingProfile()
        {
            var configMapper = new MapperConfiguration(cfg =>
            {
                CreateMap<CreateUserRequest, CreateUserCommandRequest>();

                CreateMap<UpdateUserRequest, UpdateUserCommandRequest>()
                   .ForMember(dest => dest.Id, opt => opt.Ignore());

                CreateMap<CreateUserCommandRequest, User>()
                   .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                   .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTimeOffset.UtcNow));

                CreateMap<User, CreateUserResponse>();

                CreateMap<User, UpdateUserResponse>();
            });

            Mapper = configMapper.CreateMapper();
        }
    }
}