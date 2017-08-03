using BookClubs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClubs.Data
{
    public interface IDataRepository
    {
        #region APPLICATION USERS
        List<ApplicationUser> GetAllApplicationUsers();
        void AddApplicationUser(ApplicationUser person);
        ApplicationUser GetApplicationUser(int? id);
        void UpdateProfile(ApplicationUser user);
        void RemoveApplicationUser(ApplicationUser user);
        #endregion

        #region GROUPS
        List<Group> GetAllGroups();
        List<Group> GetAllGroups(string id);
        Group GetGroup(int? id);
        void AddGroup(Group group);
        void UpdateGroup(Group group);
        void RemoveGroup(Group group);
        #endregion
    }
}
