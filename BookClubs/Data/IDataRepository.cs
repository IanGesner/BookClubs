using BookClubs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClubs.Data
{
    interface IDataRepository
    {
        #region APPLICATION USERS
        List<ApplicationUser> GetAllApplicationUsers();
        void AddApplicationUser(ApplicationUser person);
        ApplicationUser GetApplicationUser(int? id);
        void UpdateApplicationUser(ApplicationUser id);
        void RemoveApplicationUser(ApplicationUser id);
        #endregion
        #region GROUPS
        List<Group> GetAllGroups();
        List<Group> GetAllGroups(string id);
        Group GetGroup(int? id);
        void AddGroup(Group group);
        void UpdateGroup(Group id);
        void RemoveGroup(Group id);
        #endregion
    }
}
