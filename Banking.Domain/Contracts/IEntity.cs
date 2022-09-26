namespace Banking.Domain.Contracts
{
    /// <summary>
    /// Generic interface having an AccountNumber column.
    /// </summary>
    /// <typeparam name="T">The value type of AccountNumber, could be long, int, Guid.</typeparam>
    public interface IEntity<T>
    {
        /// <summary>
        /// Generic AccountNumber as the identifier of the entity.
        /// </summary>
        T AccountNumber { get; }
    }
}
