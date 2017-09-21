using BookClubs.Data.Infrastructure;
using BookClubs.Data.Repositories;
using BookClubs.Models;

namespace BookClubs.Services
{
    public interface IGroupService
    {
        //IEnumerable<Group> GetGroups(string name = null);
        Group GetGroup(int id);
        //Group GetGroup(string firstName, string lastName);
        void CreateGroup(Group group);
        void SaveGroup();
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

        //public IEnumerable<Group> GetGroups(string name)
        //{
        //    if (string.IsNullOrEmpty(name))
        //        return groupRepository.GetAll();
        //    else
        //        return groupRepository.GetAll().Where(c => c.Name == name);
        //}

        public Group GetGroup(int id)
        {
            var group = groupRepository.GetById(id);
            return group;
        }

        //public Group GetGroup(string firstName, string lastName)
        //{
        //    var group = groupRepository.GetGroupByName(firstName, lastName);
        //    return group;
        //}

        public void CreateGroup(Group group)
        {
            groupRepository.Add(group);
        }

        public void SaveGroup()
        {
            unitOfWork.Commit();
        }

        #endregion
    }
}