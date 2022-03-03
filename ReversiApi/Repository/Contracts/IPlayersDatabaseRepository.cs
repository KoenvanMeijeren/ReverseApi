namespace ReversiApi.Repository.Contracts;

/// <summary>
/// Provides an interface for games repository.
/// </summary>
public interface IPlayersDatabaseRepository<out T> : IDatabaseRepository<PlayerEntity>, IPlayersRepository<PlayerEntity>
{

}