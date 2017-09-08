using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookClubs.Models;
using System.Data.Entity;

namespace BookClubs.Data
{
    public class EfDataRepository : IDataRepository
    {
        private readonly BcContext _dbContext;
        public EfDataRepository()
        {
            _dbContext = new BcContext();
        }

        public void AddApplicationUser(User person)
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> GetAllApplicationUsers()
        {
            return _dbContext.Users;
        }

        public User GetApplicationUserById(string id)
        {
            return _dbContext.Users.Where(u => u.Id == id).FirstOrDefault();
        }
        public User GetApplicationUserByUsername(string username)
        {
            return _dbContext.Users.Where(u => u.UserName == username).FirstOrDefault();
        }

        public User GetApplicationUserByEmail(string email)
        {
            return _dbContext.Users.Where(u => u.Email == email).FirstOrDefault();
        }

        public void UpdateProfile(User user)
        {
            _dbContext.Users.Attach(user);
            _dbContext.Entry(user).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void RemoveApplicationUser(User user)
        {
            _dbContext.Users.Remove(user);
        }







        public void AddGroup(Group group)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Group> GetAllGroups()
        {
            return _dbContext.Groups;
        }

        public IQueryable<Group> GetAllGroups(string groupName)
        {
            throw new NotImplementedException();
        }

        public Group GetGroup(int? id)
        {
            Group group = _dbContext.Groups.Where(g => g.Id == id).FirstOrDefault();

            return group;
        }



        public void RemoveGroup(Group id)
        {
            throw new NotImplementedException();
        }

        public void UpdateGroup(Group id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<GroupEvent> GetAllGroupEvents()
        {
            return _dbContext.GroupEvents;
        }


    }
}