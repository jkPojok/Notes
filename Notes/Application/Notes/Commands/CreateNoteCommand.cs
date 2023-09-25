using Application.Dto;
using Application.Interfaces;
using Domain.Entities;
using MapsterMapper;
using MediatR;

namespace Application.Notes.Commands
{
    public class CreateNoteCommand : IRequest<NoteDto>
    {
        public string? Title { get; set; }
        public string Text { get; set; } = string.Empty;
    }

    public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, NoteDto>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateNoteCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<NoteDto> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            var note = new Note
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Text = request.Text,
                DateOfCreation = DateTime.UtcNow,
                DateOfEditing = DateTime.UtcNow
            };

            await _dbContext.Notes.AddAsync(note);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<NoteDto>(note);
        }
    }
}
