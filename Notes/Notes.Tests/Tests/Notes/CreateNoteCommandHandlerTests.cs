using Application.Notes.Commands;
using Microsoft.EntityFrameworkCore;
using Notes.Tests.Common;
using Xunit;

namespace Notes.Tests.Tests.Notes
{
    public class CreateNoteCommandHandlerTests : TestCQRSBase
    {
        [Fact]
        public async Task CreateNoteCommandHandlerSuccess()
        {
            //Arrange
            var handler = new CreateNoteCommandHandler(_dbContext, _mapper);
            var userId = Guid.NewGuid();
            var command = new CreateNoteCommand
            {
                UserId = userId,
                Text = "Some text",
                Title = "Some text"
            };

            //Act
            var noteDto = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.Equal(userId, noteDto.UserId);
            Assert.NotNull(_dbContext.Notes.SingleOrDefault(
                x => x.Id == noteDto.Id && 
                x.UserId == noteDto.UserId &&
                x.Title == noteDto.Title && 
                x.Text == noteDto.Text &&
                x.DateOfCreation.ToString() == noteDto.DateOfCreation &&
                x.DateOfEditing.ToString() == noteDto.DateOfEditing));
        }
    }
}
