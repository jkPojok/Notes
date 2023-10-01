using Application.Dto;
using Application.Notes.Commands;
using Application.Notes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NotesApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class NotesController : ControllerBase
    {
        private readonly ISender _mediatr;

        public NotesController(ISender mediatr, IConfiguration config) 
        {
            _mediatr = mediatr;
        }

        [HttpGet]
        public async Task<ActionResult<List<NoteDto>>> GetNotes(CancellationToken cancellationToken)
        {
            return Ok(await _mediatr.Send(new GetNotesPeviewQuery(), cancellationToken));
        }

        [HttpGet("{noteId}")]
        public async Task<ActionResult<NoteDto>> GetNote(Guid noteId, CancellationToken cancellationToken)
        {
            return Ok(await _mediatr.Send(new GetNoteQuery { Id = noteId }, cancellationToken));
        }

        [HttpPost]
        public async Task<ActionResult<NoteDto>> CreateNote([FromBody] CreateNoteCommand command, CancellationToken cancellationToken)
        {
            return Ok(await _mediatr.Send(command, cancellationToken));
        }

        [HttpPut]
        public async Task<ActionResult<NoteDto>> UpdateNote([FromBody] UpdateNoteCommand command, CancellationToken cancellationToken)
        {
            return Ok(await _mediatr.Send(command, cancellationToken));
        }

        [HttpDelete("{noteId}")]
        public async Task<ActionResult<NoteDto>> DeleteNote(Guid noteId, CancellationToken cancellationToken)
        {
            return Ok(await _mediatr.Send(new DeleteNoteCommand { Id = noteId }, cancellationToken));
        }
    }
}
