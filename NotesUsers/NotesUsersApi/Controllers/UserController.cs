using Application.Dto;
using Application.Users.Commands;
using Application.Users.Queries;
using Domain.Enums;
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

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> DeleteUser(CancellationToken cancellationToken)
        {
            return Ok(await _mediatr.Send(new DeleteUserCommand { Id = UserId }, cancellationToken));
        }

        [HttpPut] // TODO: Сделать HTTPPOST с возвращением строки, чтобы она сразу записывалась в <img />
        [Authorize]
        public async Task<ActionResult<string>> SetUserImage([FromForm] IFormFile image, CancellationToken cancellationToken)
        {
            return Ok(await _mediatr.Send(new SetUserImageCommand { UserId = UserId, Image = image }, cancellationToken));
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<PrimaryUserInfoDto>> GetPrimaryUserInfo(CancellationToken cancellationToken)
        {
            return Ok(await _mediatr.Send(new GetPrimaryUserInfoQuery { UserId = UserId }, cancellationToken));
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<string>> UpdateAboutMe(string aboutMeText, CancellationToken cancellationToken)
        {
            return Ok(await _mediatr.Send(new UpdateAboutMeCommand { UserId = UserId, AboutMe = aboutMeText}, cancellationToken));
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<Gender>> SetUserGender(Gender gender, CancellationToken cancellationToken)
        {
            return Ok(await _mediatr.Send(new SetGenderCommand { Gender = gender, UserId = UserId }, cancellationToken));
        }
    }
}
