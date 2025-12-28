using Streetcode.DAL.Entities.Streetcode;
using Streetcode.DAL.Persistence;
using Streetcode.DAL.Repositories.Interfaces.Streetcode;
using Streetcode.DAL.Repositories.Realizations.Base;

namespace Streetcode.DAL.Repositories.Realizations.Streetcode;

public class CommentsRepository : RepositoryBase<Comment>, ICommentsRepository
{
    public CommentsRepository(StreetcodeDbContext context)
        : base(context)
    {
    }
}