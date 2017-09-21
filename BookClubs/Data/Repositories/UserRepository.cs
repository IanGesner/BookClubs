using BookClubs.Data.Infrastructure;
using BookClubs.Models;
using System.Linq;

namespace BookClubs.Data.Repositories
{
    public class UserRepository : RepositoryBase<User, string>, IUserRepository
    {
        public UserRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public User GetUserByName(string firstName, string lastName)
        {
            var user = this.DbContext.Users.Where(u => u.FirstName == firstName &&
                                                        u.LastName == lastName)
                                           .FirstOrDefault();

            return user;
        }

        public override void Update(User entity)
        {
            base.Update(entity);
        }
    }

    public interface IUserRepository : IRepository<User, string>
    {
        User GetUserByName(string firstName, string lastName);
    }
}