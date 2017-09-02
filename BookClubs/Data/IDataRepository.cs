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
        void AddApplicationUser(ApplicationUser person);
        IQueryable<ApplicationUser> GetAllApplicationUsers();
        ApplicationUser GetApplicationUserById(string id);
        ApplicationUser GetApplicationUserByUsername(string username);
        ApplicationUser GetApplicationUserByEmail(string email);
        void UpdateProfile(ApplicationUser user);
        void RemoveApplicationUser(ApplicationUser user);
        #endregion

        #region GROUPS
        IQueryable<Group> GetAllGroups();
        IQueryable<Group> GetAllGroups(string id);
        Group GetGroup(int? id);
        void AddGroup(Group group);
        void UpdateGroup(Group group);
        void RemoveGroup(Group group);
        #endregion

        IQueryable<GroupEvent> GetAllGroupEvents();

    }
}
