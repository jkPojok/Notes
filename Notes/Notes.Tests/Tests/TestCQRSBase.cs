using Infrastructure.Persistence;
using MapsterMapper;
using Notes.Tests.Common;

namespace Notes.Tests.Tests
{
    public class TestCQRSBase : IDisposable
    {
        protected readonly ApplicationDbContext _dbContext;
        protected readonly IMapper _mapper = new Mapper();

        public TestCQRSBase()
        {
            _dbContext = NotesContextFactory.Create();
        }

        public void Dispose()
        {
            NotesContextFactory.Destroy(_dbContext);
        }
    }
}
