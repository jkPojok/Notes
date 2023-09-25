using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Note> Notes { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        int SaveChanges();
    }
}
