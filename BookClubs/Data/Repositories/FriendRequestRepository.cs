using BookClubs.Data.Infrastructure;
using BookClubs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookClubs.Data.Repositories
{
    public class FriendRequestRepository : RepositoryBase<FriendRequest, string>, IFriendRequestRepository
    {
        public FriendRequestRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public override void Update(FriendRequest entity)
        {
            base.Update(entity);
        }
    }

    public interface IFriendRequestRepository : IRepository<FriendRequest, string>
    {

    }
}