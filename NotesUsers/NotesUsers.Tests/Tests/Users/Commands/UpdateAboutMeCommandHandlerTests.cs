using Application.Exceptions;
using Application.Users.Commands;
using NotesUsers.Tests.Common;
using Xunit;

namespace NotesUsers.Tests.Tests.Users.Commands
{
    public class UpdateAboutMeCommandHandlerTests : TestCQRSBase
    {
        [Fact]
        public async Task UpdateAboutMeCommandHandlerThrowsNotFoundExceptionWhenThereIsNoUserInDatabase()
        {
            //Arrange
            var handler = new UpdateAboutMeCommandHandler(_dbContext);
            var command = new UpdateAboutMeCommand
            {
                UserId = Guid.NewGuid(),
                AboutMe = "Some new about me text"
            };

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task UpdateAboutMeCommandHandlerSuccess()
        {
            //Arrange
            var handler = new UpdateAboutMeCommandHandler(_dbContext);
            var command = new UpdateAboutMeCommand
            {
                UserId = UsersContextFactory.UserAId,
                AboutMe = "Some new about me text"
            };

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.Equal(result, command.AboutMe);
            Assert.NotNull(_dbContext.Users.SingleOrDefault(x =>
                x.Id == UsersContextFactory.UserAId &&
                x.AboutMe == command.AboutMe));
        }
    }
}
