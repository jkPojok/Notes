using Application.Exceptions;
using Application.Notes.Commands;
using Notes.Tests.Common;
using Xunit;

namespace Notes.Tests.Tests.Notes
{
    public class UpdateNoteCommandHandlerTests : TestCQRSBase
    {
        [Fact]
        public async Task UpdateNoteCommandHandlerThrowsNotFoundExceptionWhenThereIsNoNoteInDatabase()
        {
            //Arrange
            var handler = new UpdateNoteCommandHandler(_dbContext, _mapper);
            var command = new UpdateNoteCommand
            {
                Id = Guid.NewGuid(),
                Title = "Some new title",
                Text = "Some new text"
            };

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task UpdateNoteCommandHandlerSuccess()
        {
            //Arrange
            var handler = new UpdateNoteCommandHandler(_dbContext, _mapper);
            var command = new UpdateNoteCommand
            {
                Id = NotesContextFactory.NoteIdForUpdate,
                Text = "Some new text",
                Title = "Some new title"
            };

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.Equal(result.Id, NotesContextFactory.NoteIdForUpdate);
            Assert.Equal(result.Title, command.Title);
            Assert.Equal(result.Text, command.Text);
            Assert.NotNull(_dbContext.Notes.SingleOrDefault(
                x => x.Id == result.Id &&
                x.UserId == result.UserId &&
                x.Title == result.Title &&
                x.Text == result.Text &&
                x.DateOfCreation.ToString() == result.DateOfCreation &&
                x.DateOfEditing.ToString() == result.DateOfEditing));
        }
    }
}
