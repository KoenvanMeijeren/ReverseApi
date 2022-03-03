using Microsoft.EntityFrameworkCore;
using ReversiApi.DataAccess;
using ReversiApi.Model;
using ReversiApi.Repository.Contracts;

namespace ReversiApi.Repository;

public abstract class RepositoryDatabaseBase<T> : IDatabaseRepository<T> where T : class, IEntity
{
    protected readonly GamesDataAccess Context;

    protected RepositoryDatabaseBase(GamesDataAccess context)
    {
        this.Context = context;
    }

    /// <inheritdoc />
    public virtual void Add(T entity)
    {
        entity.Id = 0;
        this.Context.Add(entity);
        this.Context.SaveChanges();
    }

    /// <inheritdoc />
    public virtual IEnumerable<T> All()
    {
        return this.GetDbSet().ToList();
    }

    /// <inheritdoc />
    public virtual bool Exists(int id)
    {
        return this.Get(id) != null;
    }

    /// <inheritdoc />
    public virtual T? Get(int id)
    {
        bool Find(T entity) => entity.Id == id;
        
        return this.GetDbSet().Find((Func<T, bool>) Find);
    }

    /// <inheritdoc />
    public virtual bool Update(T entity)
    {
        this.GetDbSet().Update(entity);
        
        return this.Context.SaveChanges() > 0;
    }

    /// <inheritdoc />
    public virtual bool Delete(T entity)
    {
        this.GetDbSet().Remove(entity);
        
        return this.Context.SaveChanges() > 0;
    }

    /// <summary>
    /// Gets the Db Set.
    /// </summary>
    /// <returns>The Db Set</returns>
    protected abstract DbSet<T> GetDbSet();
}