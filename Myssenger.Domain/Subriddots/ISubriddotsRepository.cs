using Mysennger.Domain.Subriddots.ValueObjects;

namespace Mysennger.Domain.Subriddots;

public interface ISubriddotsRepository : IGenericRepository<Subriddot, SubriddotId>
{
    public Task<ICollection<Subriddot>> GetAllByNameLike(string name);
    public Task<bool> ExistsWithName(SubriddotName name);
}