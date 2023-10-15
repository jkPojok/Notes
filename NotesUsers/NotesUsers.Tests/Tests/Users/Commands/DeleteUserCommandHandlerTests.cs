using Application.Exceptions;
using Application.Users.Commands;
using MassTransit;
using Moq;
using NotesUsers.Tests.Common;
using Xunit;

namespace NotesUsers.Tests.Tests.Users.Commands
{
    public class DeleteUserCommandHandlerTests : TestCQRSBase
    {
        [Fact]
        public async Task DeleteUserCommandHandlerThrowsNotFoundExceptionWhenThereIsNoUserInDatabase()
        {
            //Arrange
            var handler = new DeleteUserCommandHandler(_dbContext, _mapper, new Mock<IPublishEndpoint>().Object);
            var command = new DeleteUserCommand
            {
                Id = Guid.NewGuid()
            };

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task DeleteUserCommandHandlerSuccess()
        {
            //Arrange
            var handler = new DeleteUserCommandHandler(_dbContext, _mapper, new Mock<IPublishEndpoint>().Object);
            var command = new DeleteUserCommand
            {
                Id = UsersContextFactory.UserForDeleteId
            };

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.Id, UsersContextFactory.UserForDeleteId);
            Assert.Null(_dbContext.Users.SingleOrDefault(x => x.Id == UsersContextFactory.UserForDeleteId));
        }
    }
}
