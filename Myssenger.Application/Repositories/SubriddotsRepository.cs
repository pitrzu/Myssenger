using Microsoft.EntityFrameworkCore;
using Mysennger.Domain.Subriddots;
using Mysennger.Domain.Subriddots.ValueObjects;

namespace Myssenger.Application.Repositories;

public class SubriddotsRepository : DbGenericRepository<Subriddot, SubriddotId>, ISubriddotsRepository
{
    public SubriddotsRepository(DbContext context) : base(context)
    {
    }

    public async Task<ICollection<Subriddot>> GetAllByNameLike(string name)
    {
        var results = DbSet.Where(subriddot => subriddot.SubriddotName.Value.Contains(name));
        return await results.ToListAsync();
    }

    public async Task<bool> ExistsWithName(SubriddotName name)
    {
        var result = await DbSet.FirstOrDefaultAsync(subriddot => subriddot.SubriddotName == name);
        return result == null;
    }
}