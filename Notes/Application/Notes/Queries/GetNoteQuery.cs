using Application.Dto;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Notes.Queries
{
    public class GetNoteQuery : IRequest<NoteDto>
    {
        public Guid Id { get; set; }
    }

    public class GetNoteQueryHandler : IRequestHandler<GetNoteQuery, NoteDto>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetNoteQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<NoteDto> Handle(GetNoteQuery request, CancellationToken cancellationToken)
        {
            var note = await _dbContext.Notes.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (note == null)
            {
                throw new NotFoundException(nameof(Note), request.Id);
            }

            return _mapper.Map<NoteDto>(note);
        }
    }
}
