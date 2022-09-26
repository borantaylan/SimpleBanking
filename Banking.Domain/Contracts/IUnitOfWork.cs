namespace Banking.Domain.Contracts
{
    /// <summary>
    /// Unit of work definition, defines repositories and save changes.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Fetchs account repository.
        /// </summary>
        IAccountRepository AccountRepository { get; }

        /// <summary>
        /// Save all changes as a unit, sync.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Save all changes as a unit, async.
        /// </summary>
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
