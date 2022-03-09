using ReversiApi.Model;

namespace ReversiApi.Repository.Contracts;

/// <summary>
/// Provides an interface for games repository.
/// </summary>
public interface IDatabaseRepository<T> : IRepository<T> where T : class, IEntity
{
    /// <summary>
    /// Determines if the entity exists.
    /// </summary>
    /// <param name="id">The unique id of the entity.</param>
    /// <returns>Whether the game exists or not.</returns>
    bool Exists(int id);

    /// <summary>
    /// Gets the entity.
    /// </summary>
    /// <param name="id">The unique id of the entity.</param>
    /// <returns>The entity.</returns>
    T? Get(int id);

}
