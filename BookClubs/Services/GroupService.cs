using BookClubs.Data.Infrastructure;
using BookClubs.Data.Repositories;
using BookClubs.Models;
using System.Linq;

namespace BookClubs.Services
{
    public interface IGroupService
    {
        //IEnumerable<Group> GetGroups(string name = null);
        Group GetGroup(int id);
        //Group GetGroup(string firstName, string lastName);
        void CreateGroup(Group group);
        void SaveGroup();
        IQueryable<Group> GetAll();
    }

    public class GroupService : IGroupService
    {
        private readonly IGroupRepository groupRepository;
        private readonly IUnitOfWork unitOfWork;

        public GroupService(IGroupRepository groupRepository, IUnitOfWork unitOfWork)
        {
            this.groupRepository = groupRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IGroupService Members

        public Group GetGroup(int id)
        {
            var group = groupRepository.GetById(id);
            return group;
        }

        public void CreateGroup(Group group)
        {
            groupRepository.Add(group);
        }

        public void SaveGroup()
        {
            unitOfWork.Commit();
        }

        public IQueryable<Group> GetAll()
        {
            return groupRepository.GetAll();
        }

        #endregion
    }
}