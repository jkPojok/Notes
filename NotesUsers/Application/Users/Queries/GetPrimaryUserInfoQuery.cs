using Application.Dto;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries
{
    public class GetPrimaryUserInfoQuery : IRequest<PrimaryUserInfoDto>
    {
        public Guid UserId { get; set; }
    }

    public class GetPrimaryUserInfoQueryHandler : IRequestHandler<GetPrimaryUserInfoQuery, PrimaryUserInfoDto>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetPrimaryUserInfoQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PrimaryUserInfoDto> Handle(GetPrimaryUserInfoQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }

            return _mapper.Map<PrimaryUserInfoDto>(user);
        }
    }
}
