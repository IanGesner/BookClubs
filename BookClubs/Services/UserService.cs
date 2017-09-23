using BookClubs.Data.Infrastructure;
using BookClubs.Data.Repositories;
using BookClubs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookClubs.Services
{
    public interface IUserService
    {
        //IEnumerable<User> GetUsers(string name = null);
        User GetUser(string id);
        User GetUser(string firstName, string lastName);
        void CreateUser(User user);
        void SaveUser();
        void UpdateUser(User user);
        bool AreFriends(User userOne, User userTwo);
        bool HasPendingRequest(User userOne, User userTwo);
        void AddFriendRequest(User sender, User recipient);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        #region IUserService Members

        //public IEnumerable<User> GetUsers(string name)
        //{
        //    if (string.IsNullOrEmpty(name))
        //        return userRepository.GetAll();
        //    else
        //        return userRepository.GetAll().Where(c => c.Name == name);
        //}

        public User GetUser(string id)
        {
            var user = _userRepository.GetById(id);
            return user;
        }

        public User GetUser(string firstName, string lastName)
        {
            var user = _userRepository.GetUserByName(firstName, lastName);
            return user;
        }

        public void CreateUser(User user)
        {
            _userRepository.Add(user);
        }

        public void SaveUser()
        {
            _unitOfWork.Commit();
        }

        public void UpdateUser(User user)
        {
            _userRepository.Update(user);
        }

        public bool AreFriends(User userOne, User userTwo)
        {
            return userOne.Friends.Contains(userTwo) || userTwo.Friends.Contains(userOne);
        }

        public bool HasPendingRequest(User userOne, User userTwo)
        {
            if (userOne.SentFriendRequests.Where(r => r.RecipientId == userTwo.Id).FirstOrDefault() != null ||
                    userTwo.SentFriendRequests.Where(r => r.RecipientId == userOne.Id).FirstOrDefault() != null)
                return true;
            else
                return false;
        }

        public void AddFriendRequest(User sender, User recipient)
        {
            //if (sender != null && recipient != null)
            //    sender.SentFriendRequests.Add(new FriendRequest)
        }

        #endregion
    }
}