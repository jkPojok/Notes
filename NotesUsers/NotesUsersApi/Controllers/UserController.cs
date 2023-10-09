using Application.Dto;
using Application.Users.Commands;
using Application.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetUser(CancellationToken cancellationToken)
        {
            return Ok(await _mediatr.Send(new GetUserQuery { Id = UserId }, cancellationToken));
        }

        [HttpPut] // TODO: Сделать HTTPPOST с возвращением строки, чтобы она сразу записывалась в <img />
        [Authorize]
        public async Task<ActionResult<string>> SetUserImage([FromForm] IFormFile image, CancellationToken cancellationToken)
        {
            return Ok(await _mediatr.Send(new SetUserImageCommand { UserId = UserId, Image = image }, cancellationToken));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<string>> UpdateAboutMe(string aboutMe, CancellationToken cancellationToken)
        {
            return Ok(await _mediatr.Send(new UpdateAboutMeCommand { UserId = UserId, AboutMe = aboutMe}, cancellationToken));
        }
    }
}
