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
        void Commit();
        void UpdateUser(User user);
        bool AreFriends(User userOne, User userTwo);
        bool HasReceivedRequest(User sender, User recipient);
        void AddFriendRequest(User sender, User recipient, string message);
        void AcceptRequest(User sender, User recipient);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IFriendRequestRepository _frRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, 
                            IFriendRequestRepository frRepository, 
                            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _frRepository = frRepository;
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

        public void Commit()
        {
            _unitOfWork.Commit();
        }

        public void UpdateUser(User user)
        {
            _userRepository.Update(user);
        }

        public bool AreFriends(User userOne, User userTwo)
        {
            if (userOne != null && userTwo != null)
                return userOne.Friends.Contains(userTwo) || userTwo.Friends.Contains(userOne);
            else
                return false;
        }

        public bool HasReceivedRequest(User sender, User recipient)
        {
            if (sender != null && recipient != null)
            {
                if (sender.SentFriendRequests.Where(r => r.RecipientId == recipient.Id).FirstOrDefault() != null)
                    return true;
            }

            return false;
        }

        public void AddFriendRequest(User sender, User recipient, string message)
        {
            if (sender != null && recipient != null)
            {
                //Build & add friend request
                recipient.PendingFriendRequests.Add(new FriendRequest()
                {
                    Sender = sender,
                    Body = message,
                    TimeStamp = DateTime.Now
                });

                //Update the recipient
                _userRepository.Update(recipient);
            }

        }

        public void AcceptRequest(User sender, User recipient)
        {
            if (sender != null && recipient != null)
            {
                //Delete the friend request
                _frRepository.Delete(sender.SentFriendRequests
                    .Where(r => r.RecipientId == recipient.Id)
                    .FirstOrDefault());

                //Establish friendship
                sender.Friends.Add(recipient);
            }
        }

        #endregion
    }
}