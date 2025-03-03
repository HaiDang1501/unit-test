using Domain.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Domain.Users;

public class UserRepository(ApplicationDbContext dbContext): IUserRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    public async Task<bool> Insert(User user, CancellationToken cancellationToken = default)
    {
        _dbContext.User.Add(user);

        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        return result > 0;
    }

    public async Task<User?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _dbContext.User
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync(cancellationToken);

        return result ?? null;
    }

    public async Task<IList<User>> GetAll(CancellationToken cancellationToken = default)
    {
        var result = await _dbContext.User.ToArrayAsync(cancellationToken);

        return result;
    }
}