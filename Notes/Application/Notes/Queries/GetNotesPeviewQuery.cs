using Application.Dto;
using Application.Interfaces;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Notes.Queries
{
    public class GetNotesPeviewQuery : IRequest<List<NotePreviewDto>>
    {
        
    }

    public class GetNotesPreviewQueryHandler : IRequestHandler<GetNotesPeviewQuery, List<NotePreviewDto>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetNotesPreviewQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<NotePreviewDto>> Handle(GetNotesPeviewQuery request, CancellationToken cancellationToken)
        {
            var notes = await _dbContext.Notes.ToListAsync();
            return _mapper.Map<List<NotePreviewDto>>(notes);
        }
    }
}
