using BookClubs.Models.Annotations;
using BookClubs.Models.ViewModels;
using BookClubs.Services;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;

namespace BookClubs.Controllers
{

    public class ProfilesController : Controller
    {
        private readonly IUserService _userService;
        private readonly IFriendRequestService _frService;

        public ProfilesController(IUserService userService, IFriendRequestService frService)
        {
            _userService = userService;
            _frService = frService;
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

        [OutputCache(NoStore = true, Duration = 0)]
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
                                                CurrentBookTitle = nextEvent.Book.Title,
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