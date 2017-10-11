using BookClubs.Helpers;
using BookClubs.Models;
using BookClubs.Models.Annotations;
using BookClubs.Models.ViewModels;
using BookClubs.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;

namespace BookClubs.Controllers
{
    [RequireHttps]
    [OutputCache(NoStore = true, Duration = 0)]
    public class ProfilesController : Controller
    {
        private readonly IUserService _userService;
        private readonly IFriendRequestService _frService;
        private readonly IFileManager _fileManager;

        private static readonly string _profilePicDir = ConfigurationManager.AppSettings["ProfilePicSaveDirectory"];
        private static readonly string _defaultPic = ConfigurationManager.AppSettings["DefaultProfilePicLocation"];

        public ProfilesController(IUserService userService, IFriendRequestService frService, IFileManager fileManager)
        {
            _userService = userService;
            _fileManager = fileManager;
            _frService = frService;
        }

        [HttpGet]
        public ActionResult Edit()
        {
            var userId = User.Identity.GetUserId();
            //var user = _dataRepository.GetUserById(userId);
            var user = _userService.GetUser(userId);

            EditProfileViewModel model = new EditProfileViewModel()
            {
                ProfilePictureUrl = user.ProfilePictureUrl,
                Biography = user.Biography,
                Public = user.Public
            };

            return View(model);
        }

        public ActionResult Edit(User model)
        {
            if (ModelState.IsValid)
            {
                _userService.UpdateUser(model);
                _userService.Commit();
                //_dataRepository.UpdateUser(model);
                return RedirectToAction("Index");
            }
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(EditProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Retrieve current user
                var userId = User.Identity.GetUserId();
                //var user = _dataRepository.GetUserById(userId);
                var user = _userService.GetUser(userId);

                //If it isn't the single-instance default picture, delete the current profile
                // picture from the Profile_Pictures folder
                if (!String.Equals(user.ProfilePictureUrl, _defaultPic))
                    _fileManager.DeleteFile(user.ProfilePictureUrl, Server);

                // Create a profile picture URL to save to.
                // This will map to App_data\Profile_Pictures\{User ID}.{File Extension}
                // Set the new file name to the current user's ID
                string fileName = userId + "." + _fileManager.GetFileExtension(model.ProfilePicture);
                var profilePicUrl = _fileManager.BuildPath(new string[] { _profilePicDir, fileName },
                                                ForReferenceBy.Server);

                // Save the profile picture and update the user's
                // ProfilePicUrl property in database
                string mappedPath = _fileManager.MapServerPath(profilePicUrl, Server);
                model.ProfilePicture.SaveAs(mappedPath);

                //Save changes in viewModel to user entry
                user.ProfilePictureUrl = _fileManager.ConvertPath(profilePicUrl, ForReferenceBy.Client);
                user.Biography = model.Biography;
                user.Public = model.Public;

                // Commit changes
                //_dataRepository.UpdateUser(user);
                _userService.Commit();

                return RedirectToAction("Index", "Groups");
            }

            return View(model);
        }

        // GET: Profiles
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NotificationCount()
        {
            var currentUser = _userService.GetUser(User.Identity.GetUserId());

            int count = _userService.GetNotificationCount(User.Identity.GetUserId());

            return Json(count);
        }

        [AjaxAuthorize]
        public JsonResult VerifyLoggedOn()
        {
            return Json(true);
        }

        [AjaxAuthorize]
        public JsonResult SendRequest(string id, string message)
        {
            var user = _userService.GetUser(id);
            var currentUser = _userService.GetUser(User.Identity.GetUserId());

            _userService.AddFriendRequest(currentUser, user, message);
            _userService.Commit();

            return Json(true);
        }

        [AjaxAuthorize]
        public ActionResult AcceptRequest(string id)
        {
            var user = _userService.GetUser(id);
            var currentUser = _userService.GetUser(User.Identity.GetUserId());

            _userService.AcceptRequest(user, currentUser);
            _userService.Commit();

            return Json(true);
        }

        public ActionResult Details(string id)
        {
            ICollection<GroupListItemViewModel> groupsIn = new List<GroupListItemViewModel>();
            ICollection<ProfileListViewModel> friends = new List<ProfileListViewModel>();
            string biography = null;

            var user = _userService.GetUser(id);
            var currentUser = _userService.GetUser(User.Identity.GetUserId());

            if (user != null)
            {
                bool isFriend = _userService.AreFriends(user, currentUser);
                bool isRequestSent = _userService.HasReceivedRequest(currentUser, user);
                bool isRequestReceived = _userService.HasReceivedRequest(user, currentUser);
                bool isPublic = user.Public;

                if (isPublic || isFriend)
                {
                    groupsIn = user.GroupsIn
                                    .SelectMany(group => group.GroupEvents
                                        .OrderBy(ge => ge.DateTime).Take(1), (group, nextEvent) =>
                                            new GroupListItemViewModel
                                            {
                                                Id = group.Id,
                                                GroupName = group.Name,
                                                GroupCity = group.City,
                                                GroupState = group.State,
                                                //CurrentBookTitle = nextEvent.Book.Title,
                                                MemberCount = group.Users.Count().ToString()
                                            })
                                    .ToList();

                    friends = user.Friends
                                    .Select(u =>
                                        new ProfileListViewModel
                                        {
                                            Id = u.Id,
                                            FirstName = u.FirstName,
                                            LastName = u.LastName,
                                            ProfilePictureUrl = u.ProfilePictureUrl,
                                            Biography = u.Biography
                                        })
                                    .ToList();

                    biography = user.Biography;
                }

                var viewModel = new ProfileDetailsViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Biography = biography,
                    ProfilePictureUrl = user.ProfilePictureUrl,
                    GroupsIn = groupsIn,
                    Friends = friends,
                    IsPublicProfile = isPublic,
                    IsFriend = isFriend,
                    IsRequestSent = isRequestSent,
                    IsRequestReceived = isRequestReceived
                };

                return View(viewModel);
            }

            return new HttpNotFoundResult();
        }
    }
}