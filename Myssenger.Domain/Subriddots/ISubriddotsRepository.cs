using Mysennger.Domain.Subriddots.ValueObjects;

namespace Mysennger.Domain.Subriddots;

public interface ISubriddotsRepository : IGenericRepository<Subriddot, SubriddotId>
{
    public ICollection<Subriddot> GetAllByNameLike(string name);
}