using Microsoft.AspNetCore.Mvc;
using PlagarismChecker.Application.Users.Commands.AddUser;
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
    }
}
