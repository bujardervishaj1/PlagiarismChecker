using PlagarismChecker.Application.Common.Mappings;
using PlagarismChecker.Domain.Entities;
using System;

namespace PlagarismChecker.Application.Users.Queries.GetUser
{
    public class GetUserDto : IMapFrom<User>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Username { get; set; }
    }
}
