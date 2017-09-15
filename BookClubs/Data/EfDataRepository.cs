using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookClubs.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BookClubs.Data
{
    public class EfDataRepository : IDataRepository
    {
        private readonly BcContext _dbContext;
        public EfDataRepository()
        {
            _dbContext = new BcContext();
        }

        #region USERS
        public void AddUser(User user, string password)
        {
            var userManager = new UserManager<User>(new UserStore<User>(_dbContext));
            userManager.Create(user, password);

            _dbContext.SaveChanges();
        }

        public IQueryable<User> GetAllUsers()
        {
            return _dbContext.Users;
        }

        public User GetUserById(string id)
        {
            return _dbContext.Users.Where(u => u.Id == id).FirstOrDefault();
        }
        public User GetUserByUsername(string username)
        {
            return _dbContext.Users.Where(u => u.UserName == username).FirstOrDefault();
        }

        public User GetUserByEmail(string email)
        {
            return _dbContext.Users.Where(u => u.Email == email).FirstOrDefault();
        }

        public void UpdateUser(User user)
        {
            _dbContext.Users.Attach(user);
            _dbContext.Entry(user).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void RemoveUser(User user)
        {
            _dbContext.Users.Remove(user);
        }
        #endregion
                
        #region GROUPS
        public void AddGroup(Group group)
        {
            _dbContext.Groups.Add(group);
            _dbContext.SaveChanges();
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
        
        public void AddGroupEvent(GroupEvent groupEvent)
        {
            _dbContext.GroupEvents.Add(groupEvent);
            _dbContext.SaveChanges();
        }
        public IQueryable<GroupEvent> GetAllGroupEvents()
        {
            return _dbContext.GroupEvents;
        }
        #endregion

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}