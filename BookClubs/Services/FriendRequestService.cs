using BookClubs.Data.Infrastructure;
using BookClubs.Data.Repositories;
using BookClubs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookClubs.Services
{
    public interface IFriendRequestService
    {
        void DeleteFriendRequest(FriendRequest entity);
    }
    public class FriendRequestService : IFriendRequestService
    {
        private readonly IFriendRequestRepository _repo;
        private readonly IUnitOfWork _unitOfWork;

        public FriendRequestService(IFriendRequestRepository repo, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
        }
        public void DeleteFriendRequest(FriendRequest entity)
        {
            _repo.Delete(entity);
        }
    }
}