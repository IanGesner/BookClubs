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
        User GetUser(int id);
        User GetUser(string firstName, string lastName);
        void CreateUser(User user);
        void SaveUser();
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IUserService Members

        //public IEnumerable<User> GetUsers(string name)
        //{
        //    if (string.IsNullOrEmpty(name))
        //        return userRepository.GetAll();
        //    else
        //        return userRepository.GetAll().Where(c => c.Name == name);
        //}

        public User GetUser(int id)
        {
            var user = userRepository.GetById(id);
            return user;
        }

        public User GetUser(string firstName, string lastName)
        {
            var user = userRepository.GetUserByName(firstName, lastName);
            return user;
        }

        public void CreateUser(User user)
        {
            userRepository.Add(user);
        }

        public void SaveUser()
        {
            unitOfWork.Commit();
        }

        #endregion
    }
}