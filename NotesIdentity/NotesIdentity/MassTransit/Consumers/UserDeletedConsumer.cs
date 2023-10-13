using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NotesContracts;
using NotesIdentity.Data;
using NotesIdentity.Entities;

namespace NotesIdentity.MassTransit.Consumers
{
    public class UserDeletedConsumer : IConsumer<UserDeleted>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AuthDbContext _dbContext;

        public UserDeletedConsumer(UserManager<AppUser> userManager, AuthDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<UserDeleted> context)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == context.Message.Id.ToString().ToLower());
            await _userManager.DeleteAsync(user);

            await context.RespondAsync("Data recieved succesfully");
        }
    }
}
