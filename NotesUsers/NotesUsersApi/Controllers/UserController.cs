using Application.Dto;
using Application.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NotesUsersApi.Controllers
{
    public class UserController : BaseController
    {
        private readonly ISender _mediatr;

        public UserController(ISender mediatr) 
        {
            _mediatr = mediatr;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserDto>> GetUser(Guid userId, CancellationToken cancellationToken)
        {
            return Ok(await _mediatr.Send(new GetUserQuery { Id = userId }, cancellationToken));
        }
    }
}
