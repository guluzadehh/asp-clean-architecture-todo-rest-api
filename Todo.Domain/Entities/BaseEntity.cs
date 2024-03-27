namespace Todo.Domain.Entities;

public abstract class BaseEntity
{
    private int? _id;
    public int Id
    {
        get
        {
            return _id
                ?? throw new NullReferenceException($"{this.GetType().Name} is not a database instance as it misses Id");
        }
        set { _id = value; }
    }
}
