using ReversiApi.Repository.Contracts;

namespace ReversiApi.Repository;

public abstract class RepositoryBase<T> : IRepository<T> where T : class
{
    protected readonly IList<T> Items = new List<T>();

    /// <inheritdoc />
    public virtual void Add(T entity)
    {
        this.Items.Add(entity);
    }

    /// <inheritdoc />
    public virtual IEnumerable<T> All()
    {
        return this.Items;
    }

    public virtual bool Update(T entity)
    {
        var index = this.Items.IndexOf(entity);
        this.Items[index] = entity;

        return true;
    }

    public virtual bool Delete(T entity)
    {
        return this.Items.Remove(entity);
    }
}