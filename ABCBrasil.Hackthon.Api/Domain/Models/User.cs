using System;

namespace ABCBrasil.Hackthon.Api.Domain.Models
{
    public class User
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}