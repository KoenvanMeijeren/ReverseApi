using Microsoft.EntityFrameworkCore;
using ReversiApi.DataAccess;
using ReversiApi.Model;
using ReversiApi.Repository.Contracts;

namespace ReversiApi.Repository;

public abstract class RepositoryDatabaseBase<T> : IDatabaseRepository<T> where T : class, IEntity
{
    protected readonly GamesDataAccess Context;

    protected readonly DbSet<T> DbSet;

    protected RepositoryDatabaseBase(GamesDataAccess context, DbSet<T> dbSet)
    {
        this.Context = context;
        this.DbSet = dbSet;
    }

    /// <inheritdoc />
    public virtual void Add(T entity)
    {
        this.Context.Add(entity);
        this.Context.SaveChanges();
    }

    /// <inheritdoc />
    public virtual IEnumerable<T> All()
    {
        return this.DbSet.ToList();
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
        
        return this.DbSet.Find((Func<T, bool>) Find);
    }

    /// <inheritdoc />
    public virtual bool Update(T entity)
    {
        this.DbSet.Update(entity);
        
        return this.Context.SaveChanges() > 0;
    }

    /// <inheritdoc />
    public virtual bool Delete(T entity)
    {
        this.DbSet.Remove(entity);
        
        return this.Context.SaveChanges() > 0;
    }
}