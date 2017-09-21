using BookClubs.Data;
using BookClubs.Models.ViewModels;
using BookClubs.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookClubs.Controllers
{
    public class ProfilesController : Controller
    {
        private readonly IUserService _userService;

        public ProfilesController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: Profiles
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(string id)
        {
            //var user = _dataRepository.GetUserById(id);
            //var currentUser = _dataRepository.GetUserById(User.Identity.GetUserId());
            var user = _userService.GetUser(id);
            var currentUser = _userService.GetUser(User.Identity.GetUserId());

            ICollection<GroupListItemViewModel> groupsIn = new List<GroupListItemViewModel>();
            ICollection<ProfileListViewModel> friends = new List<ProfileListViewModel>();
            string biography = null;
            ////bool publicToViewer = (user.Public || user.Friends.Contains(currentUser));
            bool isPublic = user.Public;
            bool isFriend = user.Friends.Contains(currentUser);

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
                Public = isPublic,
                Friend = isFriend
            };

            return View(viewModel);
        }
    }
}