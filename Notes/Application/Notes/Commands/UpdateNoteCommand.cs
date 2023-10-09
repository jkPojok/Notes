using Application.Dto;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Notes.Commands
{
    public class UpdateNoteCommand : IRequest<NoteDto>
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string Text { get; set; } = string.Empty;
    }

    public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, NoteDto>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateNoteCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<NoteDto> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {
            var note = await _dbContext.Notes.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (note == null)
            {
                throw new NotFoundException(nameof(Note), request.Id);
            }

            note.Title = request.Title;
            note.Text = request.Text;
            note.DateOfEditing = DateTime.Now;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<NoteDto>(note);
        }
    }
}
