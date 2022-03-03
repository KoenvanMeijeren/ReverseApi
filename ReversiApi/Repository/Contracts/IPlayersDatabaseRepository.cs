namespace ReversiApi.Repository.Contracts;

/// <summary>
/// Provides an interface for games repository.
/// </summary>
public interface IPlayersDatabaseRepository : IDatabaseRepository<PlayerEntity>, IPlayersRepository
{

}