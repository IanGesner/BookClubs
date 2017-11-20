using BookClubs.Data.Infrastructure;
using BookClubs.Models;

namespace BookClubs.Data.Repositories
{
    public class GroupWallPostRepository : RepositoryBase<GroupWallPost, int>, IGroupWallPostRepository
    {
        public GroupWallPostRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }

    public interface IGroupWallPostRepository : IRepository<GroupWallPost, int>
    {
    }
}