using Application.Exceptions;
using Application.Interfaces;
using Domain.Enums;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Commands
{
    public class SetGenderCommand : IRequest<Gender>
    {
        public Gender Gender { get; set; }
        public Guid UserId { get; set; }
    }

    public class SetGenderCommandHandler : IRequestHandler<SetGenderCommand, Gender>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public SetGenderCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Gender> Handle(SetGenderCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException(nameof(user), request.UserId);
            }

            user.Gender = request.Gender;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return request.Gender;
        }
    }
}
