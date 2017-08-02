using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookClubs.Models;

namespace BookClubs.Data
{
    public class EfDataRepository : IDataRepository
    {
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

        public void RemoveApplicationUser(ApplicationUser id)
        {
            throw new NotImplementedException();
        }

        public void RemoveGroup(Group id)
        {
            throw new NotImplementedException();
        }

        public void UpdateApplicationUser(ApplicationUser id)
        {
            throw new NotImplementedException();
        }

        public void UpdateGroup(Group id)
        {
            throw new NotImplementedException();
        }
    }
}