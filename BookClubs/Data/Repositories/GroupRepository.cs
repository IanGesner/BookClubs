using System;
using System.Linq;
using System.Linq.Expressions;
using BookClubs.Data.Infrastructure;
using BookClubs.Models;

namespace BookClubs.Data.Repositories
{
    public class GroupRepository : RepositoryBase<Group, int>, IGroupRepository
    {
        public GroupRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        //public Group GetGroupByName(string categoryName)
        //{
        //    var category = this.DbContext.Categories.Where(c => c.Name == categoryName).FirstOrDefault();

        //    return category;
        //}

        public override void Update(Group entity)
        {
            //entity.DateUpdated = DateTime.Now;
            base.Update(entity);
        }
    }

    public interface IGroupRepository : IRepository<Group, int>
    {
        //Group GetGroupByName(string categoryName);
    }
}