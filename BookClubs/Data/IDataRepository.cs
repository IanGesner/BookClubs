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
        void AddApplicationUser(User person);
        IQueryable<User> GetAllApplicationUsers();
        User GetApplicationUserById(string id);
        User GetApplicationUserByUsername(string username);
        User GetApplicationUserByEmail(string email);
        void UpdateProfile(User user);
        void RemoveApplicationUser(User user);
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
