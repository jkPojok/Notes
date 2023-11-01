using Application.Exceptions;
using Application.Users.Queries;
using NotesUsers.Tests.Common;
using Xunit;

namespace NotesUsers.Tests.Tests.Users.Queries
{
    public class GetUserQueryHandlerTests : TestCQRSBase
    {
        [Fact]
        public async Task GetUserQueryHandlerThrowsNotFoundExceptionWhenThereIsNoUserInDatabase()
        {
            //Arrange
            var handler = new GetUserQueryHandler(_dbContext, _mapper);
            var query = new GetUserQuery
            {
                Id = Guid.NewGuid()
            };

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task GetUserQueryHandlerSuccess()
        {
            //Arrange
            var handler = new GetUserQueryHandler(_dbContext, _mapper);
            var query = new GetUserQuery
            {
                Id = UsersContextFactory.UserAId
            };

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.Id, UsersContextFactory.UserAId);
            Assert.NotNull(_dbContext.Users.SingleOrDefault(x =>
            x.Id == result.Id &&
            x.Username == result.Username &&
            x.AboutMe == result.AboutMe &&
            x.UserImageLink == result.UserImageLink &&
            x.Email == result.Email &&
            x.Gender.ToString() == result.Gender &&
            x.RegistrationDate.ToString() == result.RegistrationDate));
        }
    }
}