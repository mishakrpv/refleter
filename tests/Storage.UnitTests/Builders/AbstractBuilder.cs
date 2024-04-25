namespace Storage.UnitTests.Builders;

public abstract class AbstractBuilder<TEntity> where TEntity : class
{
    protected TEntity Entity = default!;
    
    public TEntity Build()
    {
        return Entity;
    }
}