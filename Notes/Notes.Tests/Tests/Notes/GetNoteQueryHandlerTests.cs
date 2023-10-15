using Application.Exceptions;
using Application.Notes.Queries;
using Notes.Tests.Common;
using Xunit;

namespace Notes.Tests.Tests.Notes
{
    public class GetNoteQueryHandlerTests : TestCQRSBase
    {
        [Fact]
        public async Task GetNoteQueryHandlerThrowsNotFoundExceptionWhenThereIsNoNoteInDatabase()
        {
            //Arrange
            var handler = new GetNoteQueryHandler(_dbContext, _mapper);
            var query = new GetNoteQuery
            {
                Id = Guid.NewGuid()
            };

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task GetNoteQueryHandlerSuccess()
        {
            //Arrange
            var handler = new GetNoteQueryHandler(_dbContext, _mapper);
            var query = new GetNoteQuery
            {
                Id = NotesContextFactory.NoteIdForGet
            };

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Equal(result.Id, NotesContextFactory.NoteIdForGet);
            Assert.NotNull(_dbContext.Notes.SingleOrDefault(x => 
                x.Id == result.Id &&
                x.UserId == result.UserId &&
                x.Title == result.Title &&
                x.Text == result.Text &&
                x.DateOfCreation.ToString() == result.DateOfCreation &&
                x.DateOfEditing.ToString() == result.DateOfEditing));
        }
    }
}
