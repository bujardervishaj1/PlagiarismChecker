using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PlagarismChecker.Application.Users.Commands.AddUser;
using PlagarismChecker.Application.Users.Commands.LoginUser;
using PlagarismChecker.Application.Users.Queries.GetUser;
using PlagarismChecker.Application.Users.Queries.GetUserByUsername;
using PlagarismChecker.Application.Users.Queries.GetUserSearchHistory;
using System;
using System.Threading.Tasks;

namespace PlagarismChecker.WebUI.Controllers
{
    [EnableCors("MyPolicy")]
    public class UsersController : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<Guid>> AddUser(AddUserCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("login")]
        public async Task<ActionResult<Guid>> LoginUser(LoginUserCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("{username}/username")]
        public async Task<ActionResult<GetUserByUsernameDto>> GetUserByUsername(string username)
        {
            return await Mediator.Send(new GetUserByUsernameQuery { Username = username });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserDto>> GetUserById(Guid id)
        {
            return await Mediator.Send(new GetUserQuery { Id = id });
        }

        [HttpGet("{username}/searchhistory")]
        public async Task<ActionResult<GetUserSearchHistoryDto>> GetUserSearchHistory(string username)
        {
            return await Mediator.Send(new GetUserSearchHistoryQuery { Username = username });
        }
    }
}
