using Application.Dto;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Notes.Commands
{
    public class DeleteNoteCommand : IRequest<NoteDto>
    {
        public Guid Id { get; set; }
    }

    public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand, NoteDto>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public DeleteNoteCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<NoteDto> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
        {
            var note = await _dbContext.Notes.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (note == null) 
            {
                throw new NotFoundException(nameof(Note), request.Id);
            }

            _dbContext.Notes.Remove(note);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<NoteDto>(note);
        }
    }
}
