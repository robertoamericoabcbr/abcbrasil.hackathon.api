using ABCBrasil.Hackthon.Api.Domain.CommandQuerySeparation.Commands;
using ABCBrasil.Hackthon.Api.Domain.DataContracts.Requests.Users;
using ABCBrasil.Hackthon.Api.Domain.DataContracts.Responses.Users;
using AutoBogus;
using AutoBogus.Conventions;

namespace ABCBrasil.Hackthon.Api.UnitTests
{
    public static class SourcesBogus
    {
        internal static CreateUserCommandRequest GenerateCreateUserCommandRequest201Created()
        {
            return new AutoFaker<CreateUserCommandRequest>().Configure(builder => { builder.WithConventions(); }).Generate();
        }

        internal static CreateUserRequest GenerateCreateUserRequest201Created()
        {
            return new AutoFaker<CreateUserRequest>().Configure(builder => { builder.WithConventions(); }).Generate();
        }

        internal static CreateUserRequest GenerateCreateUserRequest400BadRequest()
        {
            var createUserRequest = new AutoFaker<CreateUserRequest>().Configure(builder => { builder.WithConventions(); }).Generate();

            createUserRequest.Name = null;

            return createUserRequest;
        }

        internal static CreateUserResponse GenerateCreateUserResponse201Created()
        {
            return new AutoFaker<CreateUserResponse>().Configure(builder => { builder.WithConventions(); }).Generate();
        }

        internal static UpdateUserCommandRequest GenerateUpdateUserCommandRequest201Created()
        {
            return new AutoFaker<UpdateUserCommandRequest>().Configure(builder => { builder.WithConventions(); }).Generate();
        }

        internal static UpdateUserRequest GenerateUpdateUserRequest201Created()
        {
            return new AutoFaker<UpdateUserRequest>().Configure(builder => { builder.WithConventions(); }).Generate();
        }

        internal static UpdateUserRequest GenerateUpdateUserRequest400BadRequest()
        {
            var updateUserRequest = new AutoFaker<UpdateUserRequest>().Configure(builder => { builder.WithConventions(); }).Generate();

            updateUserRequest.Name = null;

            return updateUserRequest;
        }

        internal static UpdateUserResponse GenerateUpdateUserResponse201Created()
        {
            return new AutoFaker<UpdateUserResponse>().Configure(builder => { builder.WithConventions(); }).Generate();
        }
    }
}