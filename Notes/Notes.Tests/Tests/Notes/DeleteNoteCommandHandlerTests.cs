using Application.Exceptions;
using Application.Notes.Commands;
using Notes.Tests.Common;
using Xunit;

namespace Notes.Tests.Tests.Notes
{
    public class DeleteNoteCommandHandlerTests : TestCQRSBase
    {
        [Fact]
        public async Task DeleteNoteCommandHandlerThrowsNotFoundExceptionWhenThereIsNoNoteInDatabase()
        {
            //Arrange
            var handler = new DeleteNoteCommandHandler(_dbContext, _mapper);
            var command = new DeleteNoteCommand
            {
                Id = Guid.NewGuid()
            };

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task DeleteNoteCommandHandlerSuccess()
        {
            //Arrange
            var handler = new DeleteNoteCommandHandler(_dbContext, _mapper);
            var command = new DeleteNoteCommand
            {
                Id = NotesContextFactory.NoteIdForDelete
            };

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.Equal(result.Id, NotesContextFactory.NoteIdForDelete);
            Assert.Null(_dbContext.Notes.SingleOrDefault(x => x.Id == NotesContextFactory.NoteIdForDelete));
        }
    }
}
