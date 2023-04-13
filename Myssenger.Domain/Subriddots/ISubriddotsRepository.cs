using Mysennger.Domain.Subriddots.ValueObjects;

namespace Mysennger.Domain.Subriddots;

public interface ISubriddotsRepository : IRepository<Subriddot, SubriddotId>
{
    public Task<IEnumerable<Subriddot>> GetAllBySimilarName(string name);
}