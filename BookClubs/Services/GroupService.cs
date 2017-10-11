using BookClubs.Data.Infrastructure;
using BookClubs.Data.Repositories;
using BookClubs.Helpers;
using BookClubs.Models;
using System;
using System.Configuration;
using System.Linq;
using System.Web;

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
        void CreateGroup(Group group, HttpPostedFileBase groupPicture, HttpServerUtilityBase httpServerUtilityBase);
    }

    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager _fileManager;

        private static readonly string _defaultGroupPicPath = ConfigurationManager.AppSettings["DefaultGroupPicLocation"];
        private static readonly string _customGroupPicsPath = ConfigurationManager.AppSettings["GroupProfilePicSaveDirectory"];

        public GroupService(IGroupRepository groupRepository, IUnitOfWork unitOfWork, IFileManager fileManager)
        {
            _groupRepository = groupRepository;
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
        }

        #region IGroupService Members

        public Group GetGroup(int id)
        {
            var group = _groupRepository.GetById(id);
            return group;
        }

        public void CreateGroup(Group group)
        {
            _groupRepository.Add(group);

            _unitOfWork.Commit();
        }

        public void SaveGroup()
        {
            _unitOfWork.Commit();
        }

        public IQueryable<Group> GetAll()
        {
            return _groupRepository.GetAll();
        }
        
        public void CreateGroup(Group group, HttpPostedFileBase groupPicture, HttpServerUtilityBase httpServerUtilityBase)
        {
            if (group != null)
            {
                _groupRepository.Add(group);
                _unitOfWork.Commit();       // Add the group to the database first - need the Id for picture saving

                if (groupPicture != null && httpServerUtilityBase != null)   // If the user uploaded a custom picture for the group
                {
                    string fileName = group.Id + "." +
                        _fileManager.GetFileExtension(groupPicture); // Build file name {groupId}.{ext}

                    var groupPicPath = _fileManager.BuildPath(new string[] { _customGroupPicsPath, fileName },
                                                                ForReferenceBy.Server); // Build relative path

                    string mappedPath = _fileManager.MapServerPath(groupPicPath, httpServerUtilityBase);    // Build actual path
                    groupPicture.SaveAs(mappedPath);    // Save to actual path

                    group.GroupPictureUrl = _fileManager.ConvertPath(groupPicPath, ForReferenceBy.Client); // Convert \'s to /'s and save path to DB

                    _groupRepository.Update(group);

                    _unitOfWork.Commit();   // Re-commit with the image path.
                }
            }
        }

        #endregion
    }
}