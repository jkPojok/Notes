using Application.Users.Commands;
using MapsterMapper;
using MassTransit;
using MediatR;
using NotesContracts;

namespace NotesUsersApi.MassTransit.Consumers
{
    public class UserCreatedConsumer : IConsumer<UserCreated>
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediatr;

        public UserCreatedConsumer(IMapper mapper, ISender mediatr)
        {
            _mapper = mapper;
            _mediatr = mediatr;
        }

        public async Task Consume(ConsumeContext<UserCreated> context)
        {
            CreateUserCommand createUserCommand = _mapper.Map<CreateUserCommand>(context.Message);
            await _mediatr.Send(createUserCommand);

            await context.RespondAsync("Data recieved succesfully");
        }
    }
}
