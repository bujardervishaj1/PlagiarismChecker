using PlagarismChecker.Application.Common.Mappings;
using PlagarismChecker.Domain.Entities;
using System;

namespace PlagarismChecker.Application.Users.Queries.GetUserByUsername
{
    public class GetUserByUsernameDto : IMapFrom<User>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Username { get; set; }
    }
}
