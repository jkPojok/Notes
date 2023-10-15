using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace NotesUsers.Tests.Common
{
    public class UsersContextFactory
    {
        public static Guid UserAId = Guid.NewGuid();
        public static Guid UserBId = Guid.NewGuid();
        public static Guid UserForDeleteId = Guid.NewGuid();

        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Database.EnsureCreated();
            dbContext.Users.AddRange(
                new User
                {
                    Id = UserAId,
                    Username = "User A",
                    Email = "UserA@test.com",
                    Gender = Gender.Male,
                    AboutMe = "Some about me text",
                    RegistrationDate = DateTime.Now
                },
                new User
                {
                    Id = UserBId,
                    Username = "User B",
                    Email = "UserB@test.com",
                    Gender = Gender.Male,
                    AboutMe = "Some about me text",
                    RegistrationDate = DateTime.Now
                },
                new User
                {
                    Id = UserForDeleteId,
                    Username = "User for delete",
                    Email = "UserForDelete@test.com",
                    Gender = Gender.Male,
                    AboutMe = "Some about me text",
                    RegistrationDate = DateTime.Now
                });

            dbContext.SaveChanges();

            return dbContext;
        }

        public static void Destroy(ApplicationDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();
            dbContext.Dispose();
        }
    }
}
