using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookClubs.Models;

namespace BookClubs.Data
{
    public class EfDataRepository : IDataRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public EfDataRepository()
        {
            _dbContext = new ApplicationDbContext();
        }

        public void AddApplicationUser(ApplicationUser person)
        {
            throw new NotImplementedException();
        }

        public List<ApplicationUser> GetAllApplicationUsers()
        {
            return _dbContext.Users.ToList();
        }

        public ApplicationUser GetApplicationUserById(string id)
        {
            return _dbContext.Users.Where(u => u.Id == id).FirstOrDefault();
        }
        public ApplicationUser GetApplicationUserByUsername(string username)
        {
            return _dbContext.Users.Where(u => u.UserName == username).FirstOrDefault();
        }

        public ApplicationUser GetApplicationUserByEmail(string email)
        {
            return _dbContext.Users.Where(u => u.Email == email).FirstOrDefault();
        }

        public void UpdateProfile(ApplicationUser user)
        {
            ApplicationUser userToUpdate = _dbContext.Users.Where(u => u.Id == user.Id).FirstOrDefault();

            //if FirstOrDefault() finds a user matching the ID, set the first name, last name, and biography
            if (userToUpdate != null)
            {
                userToUpdate.FirstName = user.FirstName;
                userToUpdate.LastName = user.LastName;
                userToUpdate.Biography = user.Biography;

                _dbContext.SaveChanges();
            }
        }

        public void RemoveApplicationUser(ApplicationUser user)
        {
            _dbContext.Users.Remove(user);
        }







        public void AddGroup(Group group)
        {
            throw new NotImplementedException();
        }

        public List<Group> GetAllGroups()
        {
            return _dbContext.Groups.ToList();
        }

        public List<Group> GetAllGroups(string groupName)
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

        public List<GroupEvent> GetAllGroupEvents()
        {
            return _dbContext.GroupEvents.ToList();
        }


    }
}