using Application.Dto;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries
{
    public class GetUserQuery : IRequest<UserDto>
    {
        public Guid Id { get; set; }
    }

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            return _mapper.Map<UserDto>(user);
        }
    }
}
