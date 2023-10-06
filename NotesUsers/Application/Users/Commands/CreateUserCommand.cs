using Application.Dto;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MapsterMapper;
using MediatR;

namespace Application.Users.Commands
{
    public class CreateUserCommand : IRequest<UserDto>
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Id = request.Id,
                Username = request.Username,
                RegistrationDate = DateTime.UtcNow,
                Gender = Gender.NotSpecified
            };

            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<UserDto>(user);
        }
    }
}
