﻿namespace Domain.Users;

public interface IUserRepository
{
    public Task<bool> Insert(User user, CancellationToken cancellationToken = default);

    public Task<User?> GetById(Guid id, CancellationToken cancellationToken = default);

    public Task<IList<User>> GetAll(CancellationToken cancellationToken = default);
}