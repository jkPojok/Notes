using Infrastructure.Persistence;
using MapsterMapper;
using NotesUsers.Tests.Common;

namespace NotesUsers.Tests.Tests
{
    public class TestCQRSBase : IDisposable
    {
        protected readonly ApplicationDbContext _dbContext;
        protected readonly IMapper _mapper = new Mapper();

        public TestCQRSBase()
        {
            _dbContext = UsersContextFactory.Create();
        }

        public void Dispose()
        {
            UsersContextFactory.Destroy(_dbContext);
        }
    }
}
