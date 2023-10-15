using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Notes.Tests.Common
{
    public class NotesContextFactory
    {
        public static Guid UserAId = Guid.NewGuid();
        public static Guid UserBId = Guid.NewGuid();
        public static Guid NoteIdForDelete = Guid.NewGuid();
        public static Guid NoteIdForUpdate = Guid.NewGuid();
        public static Guid NoteIdForGet = Guid.NewGuid();

        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Database.EnsureCreated();
            dbContext.Notes.AddRange(
                new Note
                {
                    Id = NoteIdForDelete,
                    UserId = UserAId,
                    Title = "Some title",
                    Text = "Some text",
                    DateOfCreation = DateTime.Now,
                    DateOfEditing = DateTime.Now
                },
                new Note
                {
                    Id = NoteIdForUpdate,
                    UserId = UserBId,
                    Title = "Some title",
                    Text = "Some text",
                    DateOfCreation = DateTime.Now,
                    DateOfEditing = DateTime.Now
                },
                new Note
                {
                    Id = NoteIdForGet,
                    UserId = UserAId,
                    Title = "Some title",
                    Text = "Some text",
                    DateOfCreation = DateTime.Now,
                    DateOfEditing = DateTime.Now
                },
                new Note
                {
                    Id = Guid.NewGuid(),
                    UserId = UserBId,
                    Title = "Some title",
                    Text = "Some text",
                    DateOfCreation = DateTime.Now,
                    DateOfEditing = DateTime.Now
                });
            dbContext.SaveChanges();

            return dbContext;
        }

        public static void Destroy(ApplicationDbContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }
    }
}
