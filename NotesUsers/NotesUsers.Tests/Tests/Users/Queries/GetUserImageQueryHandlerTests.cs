using Application.Exceptions;
using Application.Users.Queries;
using NotesUsers.Tests.Common;
using Xunit;

namespace NotesUsers.Tests.Tests.Users.Queries
{
    public class GetUserImageQueryHandlerTests : TestCQRSBase
    {
        [Fact]
        public async Task GetUserImageQueryHandlerThrowsNotFoundExceptionWhenThereIsNoUserInDatabase()
        {
            //Arrange
            var handler = new GetUserImageQueryHandler(_dbContext);
            var query = new GetUserImageQuery
            {
                UserId = Guid.NewGuid()
            };

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task GetUserImageQueryHandlerSuccess()
        {
            //Arrange
            var handler = new GetUserImageQueryHandler(_dbContext);
            var query = new GetUserImageQuery
            {
                UserId = UsersContextFactory.UserBId
            };

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(_dbContext.Users.SingleOrDefault(x => 
                x.UserImageLink == result &&
                x.Id == UsersContextFactory.UserBId));
        }
    }
}
