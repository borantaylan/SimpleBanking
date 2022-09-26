namespace Banking.Domain.Contracts
{
    /// <summary>
    /// Generic interface having an Identifier column.
    /// </summary>
    /// <typeparam name="T">The value type of Identifier, could be long, int, Guid.</typeparam>
    public interface IEntity<T>
    {
        /// <summary>
        /// Generic Identifier as the identifier of the entity.
        /// </summary>
        T Identifier { get; }
    }
}
