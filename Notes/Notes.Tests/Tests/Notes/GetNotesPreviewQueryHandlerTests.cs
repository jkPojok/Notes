using Application.Notes.Queries;
using Notes.Tests.Common;
using Shouldly;
using Xunit;

namespace Notes.Tests.Tests.Notes
{
    public class GetNotesPreviewQueryHandlerTests : TestCQRSBase
    {
        [Fact]
        public async Task GetNotesPreviewQueryHandlerSuccess()
        {
            //Arrange
            var handler = new GetNotesPreviewQueryHandler(_dbContext, _mapper);
            var query = new GetNotesPeviewQuery
            {
                UserId = NotesContextFactory.UserAId
            };

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Count.ShouldBe(2);
        }
    }
}
