using Application.Dto;
using Application.Notes.Commands;
using Application.Notes.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace NotesApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class NotesController : ControllerBase
    {
        private readonly ISender _mediatr;

        private Guid UserId => !User.Identity.IsAuthenticated
            ? Guid.Empty 
            : Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        public NotesController(ISender mediatr, IConfiguration config) 
        {
            _mediatr = mediatr;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<NoteDto>>> GetNotes(CancellationToken cancellationToken)
        {
            return Ok(await _mediatr.Send(new GetNotesPeviewQuery { UserId = UserId }, cancellationToken));
        }

        [HttpGet("{noteId}")]
        [Authorize]
        public async Task<ActionResult<NoteDto>> GetNote(Guid noteId, CancellationToken cancellationToken)
        {
            return Ok(await _mediatr.Send(new GetNoteQuery { Id = noteId }, cancellationToken));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<NoteDto>> CreateNote([FromBody] CreateNoteCommand command, CancellationToken cancellationToken)
        {
            return Ok(await _mediatr.Send(new CreateNoteCommand 
            {
                UserId = UserId,
                Text = command.Text,
                Title = command.Title 
            }, cancellationToken));
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<NoteDto>> UpdateNote([FromBody] UpdateNoteCommand command, CancellationToken cancellationToken)
        {
            return Ok(await _mediatr.Send(command, cancellationToken));
        }

        [HttpDelete("{noteId}")]
        [Authorize]
        public async Task<ActionResult<NoteDto>> DeleteNote(Guid noteId, CancellationToken cancellationToken)
        {
            return Ok(await _mediatr.Send(new DeleteNoteCommand { Id = noteId }, cancellationToken));
        }
    }
}
