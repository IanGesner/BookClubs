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

        public void AddGroup(Group group)
        {
            throw new NotImplementedException();
        }

        public List<ApplicationUser> GetAllApplicationUsers()
        {
            throw new NotImplementedException();
        }

        public void UpdateProfile(ApplicationUser user)
        {
            ApplicationUser userToUpdate = _dbContext.Users.Where(u => u.Id == user.Id).FirstOrDefault();

            if (userToUpdate != null)
            {
                userToUpdate.FirstName = user.FirstName;
                userToUpdate.LastName = user.LastName;
                userToUpdate.Biography = user.Biography;

                _dbContext.SaveChanges();
            }
        }



        public List<Group> GetAllGroups()
        {
            throw new NotImplementedException();
        }

        public List<Group> GetAllGroups(string id)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser GetApplicationUser(int? id)
        {
            throw new NotImplementedException();
        }

        public Group GetGroup(int? id)
        {
            throw new NotImplementedException();
        }

        public void RemoveApplicationUser(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public void RemoveGroup(Group id)
        {
            throw new NotImplementedException();
        }

        public void UpdateGroup(Group id)
        {
            throw new NotImplementedException();
        }
    }
}