using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries
{
    public class GetUserImageQuery : IRequest<string>
    {
        public Guid UserId { get; set; }
    }

    public class GetUserImageQueryHandler : IRequestHandler<GetUserImageQuery, string>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetUserImageQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> Handle(GetUserImageQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }

            var userImageLink = user.UserImageLink;

            return userImageLink;
        }
    }
}
