using ABCBrasil.Hackthon.Api.Controllers;
using ABCBrasil.Hackthon.Api.Domain.CommandQuerySeparation.Commands;
using ABCBrasil.Hackthon.Api.Domain.CommandQuerySeparation.Queries;
using ABCBrasil.Hackthon.Api.Domain.DataContracts.Requests.Users;
using ABCBrasil.Hackthon.Api.Domain.DataContracts.Responses;
using ABCBrasil.Hackthon.Api.Domain.DataContracts.Responses.Users;
using ABCBrasil.Hackthon.Api.Infra.Extensions;
using ABCBrasil.Providers.BasicContractProvider.Lib;
using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ABCBrasil.Hackthon.Api.UnitTests.Controllers
{
    public class UsersControllerTests
    {
        private readonly UsersController _controller;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly INotificationProvider _notificationProvider;

        public UsersControllerTests()
        {
            _notificationProvider = new NotificationProvider();
            _mapperMock = new Mock<IMapper>();
            _mediatorMock = new Mock<IMediator>();

            _controller = new UsersController(
                _notificationProvider,
                _mapperMock.Object,
                _mediatorMock.Object);
        }

        [Fact(DisplayName = "Excluir usuário com sucesso")]
        public async Task Delete_Should_Return_204NoContent_When_RequestIsValid()
        {
            _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteUserCommandQuery>(), CancellationToken.None))
                    .ReturnsAsync(new ApiResponse<bool>(It.IsAny<bool>()));

            //act
            IActionResult result = await _controller.Delete(It.IsAny<DeleteUserCommandQuery>());

            ObjectResult? objectResult = result as ObjectResult;

            //assert
            result.Should().NotBeNull();
            Assert.True(objectResult is null);
        }

        [Fact(DisplayName = "Excluir usuário com recurso não encontrado")]
        public async Task Delete_Should_Return_404NotFound_When_ResourceNotFound()
        {
            var notifications = _notificationProvider.CreateNotification().AddNotFound();

            _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteUserCommandQuery>(), CancellationToken.None))
                     .ReturnsAsync(notifications.GetNotifications());

            //act
            IActionResult result = await _controller.Delete(It.IsAny<DeleteUserCommandQuery>());

            ObjectResult? objectResult = result as ObjectResult;

            //assert
            Assert.True(objectResult is not null && objectResult.StatusCode.Equals(StatusCodes.Status404NotFound));
        }

        [Fact(DisplayName = "Obter usuário com sucesso")]
        public async Task Get_Should_Return_200Ok_When_RequestIsValid()
        {
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetUserCommandQuery>(), CancellationToken.None))
                    .ReturnsAsync(new ApiResponse<CreateUserResponse>(It.IsAny<CreateUserResponse>()));

            //act
            IActionResult result = await _controller.Get(It.IsAny<GetUserCommandQuery>());

            ObjectResult? objectResult = result as ObjectResult;

            //assert
            result.Should().NotBeNull();
            Assert.True(objectResult is not null && objectResult.StatusCode.Equals(StatusCodes.Status200OK));
        }

        [Fact(DisplayName = "Obter usuário com recurso não encontrado")]
        public async Task Get_Should_Return_404NotFound_When_ResourceNotFound()
        {
            var notifications = _notificationProvider.CreateNotification().AddNotFound();

            _mediatorMock.Setup(x => x.Send(It.IsAny<GetUserCommandQuery>(), CancellationToken.None))
                     .ReturnsAsync(notifications.GetNotifications());

            //act
            IActionResult result = await _controller.Get(It.IsAny<GetUserCommandQuery>());

            ObjectResult? objectResult = result as ObjectResult;

            //assert
            Assert.True(objectResult is not null && objectResult.StatusCode.Equals(StatusCodes.Status404NotFound));
        }

        [Fact(DisplayName = "Criação do usuário com sucesso")]
        public async Task Post_Should_Return_201Created_When_RequestIsValid()
        {
            // Arrange
            var request = SourcesBogus.GenerateCreateUserRequest201Created();

            _mapperMock.Setup(m => m.Map<CreateUserCommandRequest>(It.IsAny<CreateUserRequest>()))
                 .Returns(SourcesBogus.GenerateCreateUserCommandRequest201Created());

            _mediatorMock.Setup(x => x.Send(It.IsAny<CreateUserCommandRequest>(), CancellationToken.None))
                     .ReturnsAsync(new ApiResponse<CreateUserResponse>(SourcesBogus.GenerateCreateUserResponse201Created()));

            // Act
            var result = await _controller.Post(request) as CreatedResult;

            // Assert
            result.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status201Created);
            var response = result?.Value as ApiResponse<CreateUserResponse>;
            response.Should().NotBeNull();
        }

        [Fact(DisplayName = "Criação do usuário com contrato inválido")]
        public async Task Post_Should_Return_400BadRequest_When_ModelStateIsInvalid()
        {
            // Arrange
            var request = SourcesBogus.GenerateCreateUserRequest400BadRequest();

            _controller.ModelState.AddModelError("Name", "The Name field is required.");

            // Act
            var result = await _controller.Post(request) as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact(DisplayName = "Atualização do usuário com sucesso")]
        public async Task Put_Should_Return_201Created_When_RequestIsValid()
        {
            // Arrange
            var request = SourcesBogus.GenerateUpdateUserRequest201Created();

            _mapperMock.Setup(m => m.Map<UpdateUserCommandRequest>(It.IsAny<UpdateUserRequest>()))
                 .Returns(SourcesBogus.GenerateUpdateUserCommandRequest201Created());

            _mediatorMock.Setup(x => x.Send(It.IsAny<UpdateUserCommandRequest>(), CancellationToken.None))
                     .ReturnsAsync(new ApiResponse<UpdateUserResponse>(SourcesBogus.GenerateUpdateUserResponse201Created()));

            // Act
            var result = await _controller.Put(Guid.NewGuid().ToString(), request) as CreatedResult;

            // Assert
            result.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status201Created);
            var response = result?.Value as ApiResponse<UpdateUserResponse>;
            response.Should().NotBeNull();
        }

        [Fact(DisplayName = "Atualização do usuário com contrato inválido")]
        public async Task Put_Should_Return_400BadRequest_When_ModelStateIsInvalid()
        {
            // Arrange
            var request = SourcesBogus.GenerateUpdateUserRequest400BadRequest();

            _controller.ModelState.AddModelError("Name", "The Name field is required.");

            // Act
            var result = await _controller.Put(Guid.NewGuid().ToString(), request) as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result?.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }
    }
}