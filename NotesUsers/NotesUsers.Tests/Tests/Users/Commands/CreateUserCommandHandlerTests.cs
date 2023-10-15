using Application.Users.Commands;
using Xunit;

namespace NotesUsers.Tests.Tests.Users.Commands
{
    public class CreateUserCommandHandlerTests : TestCQRSBase
    {
        [Fact]
        public async Task CreateUserCommandHandlerSuccess()
        {
            //Arrange
            var handler = new CreateUserCommandHandler(_dbContext, _mapper);
            var command = new CreateUserCommand
            {
                Id = Guid.NewGuid(),
                Username = "User for create",
                Email = "UserForCreate@test.com"
            };

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(command.Id, result.Id);
            Assert.NotNull(_dbContext.Users.SingleOrDefault(x =>
            x.Id == result.Id &&
            x.Username == result.Username &&
            x.AboutMe == result.AboutMe &&
            x.Email == result.Email &&
            x.UserImageLink == result.UserImageLink &&
            x.Gender.ToString() == result.Gender &&
            x.RegistrationDate.ToString() == result.RegistrationDate));
        }
    }
}
