using Microsoft.AspNetCore.Mvc;
using PlagarismChecker.Application.Users.Commands.AddUser;
using PlagarismChecker.Application.Users.Queries.GetUserByUsername;
using System;
using System.Threading.Tasks;

namespace PlagarismChecker.WebUI.Controllers
{
    public class UsersController : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<Guid>> AddUser(AddUserCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("{username}/username")]
        public async Task<ActionResult<GetUserByUsernameDto>> GetUserByUsername(string username)
        {
            return await Mediator.Send(new GetUserByUsernameQuery { Username = username });
        }
    }
}
