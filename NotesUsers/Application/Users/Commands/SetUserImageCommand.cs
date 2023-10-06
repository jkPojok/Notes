using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Commands
{
    public class SetUserImageCommand : IRequest<string>
    {
        public Guid UserId { get; set; }
        public string? ImageLink { get; set; }
    }

    public class SetUserImageCommandHandler : IRequestHandler<SetUserImageCommand, string>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public SetUserImageCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<string> Handle(SetUserImageCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }

            user.UserImageLink = request.ImageLink;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return request.ImageLink;
        }
    }
}
