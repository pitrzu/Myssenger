namespace Mysennger.Domain;

public interface IUnitOfWork 
{
    
    public Task Commit(CancellationToken cancellationToken = new());
    public Task Rollback();
}