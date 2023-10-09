using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Commands
{
    public class UpdateAboutMeCommand : IRequest<string?>
    {
        public Guid UserId { get; set; }
        public string? AboutMe { get; set; }
    }

    public class UpdateAboutMeCommandHandler : IRequestHandler<UpdateAboutMeCommand, string?>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdateAboutMeCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string?> Handle(UpdateAboutMeCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }

            user.AboutMe = request.AboutMe;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return user.AboutMe;
        }
    }
}
