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
        void AddUser(User person, string password);
        IQueryable<User> GetAllUsers();
        User GetUserById(string id);
        User GetUserByUsername(string username);
        User GetUserByEmail(string email);
        void UpdateUser(User user);
        void RemoveUser(User user);
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

        void Save();

    }
}
