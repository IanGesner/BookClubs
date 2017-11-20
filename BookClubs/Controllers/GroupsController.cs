using BookClubs.Data;
using BookClubs.Helpers;
using BookClubs.Models;
using BookClubs.Models.ViewModels;
using BookClubs.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookClubs.Controllers
{
    [RequireHttps]
    [Authorize]
    [OutputCache(NoStore = true, Duration = 0)]
    public class GroupsController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly IUserService _userService;
        private readonly IFileManager _fileManager;

        private static readonly string _defaultGroupPicPath = ConfigurationManager.AppSettings["DefaultGroupPicLocation"];

        public GroupsController(IGroupService groupService, IUserService userService, IFileManager fileManager)
        {
            _userService = userService;
            _groupService = groupService;
            _fileManager = fileManager;
        }

        // GET: Groups
        [AllowAnonymous]
        public ActionResult Index()
        {
            // Get a list of all groups
            var viewModel = _groupService.GetAll().Select(group => new GroupListItemViewModel
            {
                Id = group.Id,
                GroupName = group.Name,
                GroupCity = group.City,
                GroupState = group.State,
                MemberCount = group.Users.Count.ToString()
            });

            return View(viewModel);
        }

        // GET: Groups/Details/5
        public ActionResult Details(int id)
        {
            // Retrieve the current user and the group they are viewing
            var group = _groupService.GetGroup(id);
            var currentUserId = User.Identity.GetUserId();

            // Retrieve member profiles for the group
            var memberProfiles = group.Users.Select(u => new ProfileListViewModel()
            {
                Id = u.Id,
                Biography = u.Biography,
                FirstName = u.FirstName,
                LastName = u.LastName,
                ProfilePictureUrl = u.ProfilePictureUrl
            })
                .ToList();

            // Retrieve posts and replies on group wall
            var wallPosts = group.GroupWallPosts.Select(gwp => new GroupWallPostListViewModel()
            {
                Id = gwp.Id,
                PosterId = gwp.PosterId,
                Body = gwp.Body,
                DateTime = gwp.TimeStamp.ToShortTimeString(),
                PosterName = gwp.Poster.FirstName + " " + gwp.Poster.LastName,
                ProfilePictureUrl = _userService.GetUser(gwp.PosterId).ProfilePictureUrl,
                Replies = gwp.Replies.Select(gwpr => new GroupWallPostReplyViewModel()
                {
                    Id = gwpr.Id,
                    Body = gwpr.Body,
                    PosterId = gwpr.PosterId,
                    PosterName = gwpr.Poster.FirstName + " " + gwpr.Poster.LastName,
                    DateTime = gwpr.TimeStamp.ToShortTimeString(),
                    ProfilePictureUrl = _userService.GetUser(gwpr.PosterId).ProfilePictureUrl
                })
            })
                .ToList();

            // Retrieve events scheduled for this group
            var groupEvents = group.GroupEvents.Select(ge => new GroupEventListViewModel()
            {
                Id = ge.Id,
                BookName = ge.Book.Title,
                DateTime = ge.DateTime.ToLongTimeString(),
                Location = ge.City + ", " + ge.State
            })
                .OrderBy(ge => ge.DateTime)
                .ToList();

            // Verify the user is a member of the specified group.
            var member = group.Users.Where(u => u.Id == currentUserId)
                                    .FirstOrDefault();

            var isMember = (member == null ? false : true);

            if (group != null)
            {
                var viewModel = new GroupDetailsViewModel
                {
                    Id = group.Id,
                    GroupName = group.Name,
                    GroupState = group.State,
                    GroupCity = group.City,
                    //CurrentBookTitle = group.GroupEvents.FirstOrDefault().Book.Title,
                    MemberCount = group.Users.Count.ToString(),
                    MemberProfiles = memberProfiles,
                    ProfilePictureUrl = group.GroupPictureUrl,
                    WallPosts = wallPosts,
                    IsOrganizer = (group.OrganizerId == currentUserId),
                    CurrentUserId = currentUserId,
                    GroupEvents = groupEvents,
                    IsMember = isMember,
                    IsPublic = group.Public
                };

                return View(viewModel);
            }
            else
                return new HttpNotFoundResult("We couldn't find the group you requested.");
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            var model = new GroupCreateViewModel();
            model.GroupPictureUrl = _defaultGroupPicPath;

            return View(model);
        }

        // POST: Groups/Create
        [HttpPost]
        public ActionResult Create(GroupCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Retrieve current user
                var userId = User.Identity.GetUserId();

                // Save the uploaded picture to the file system in
                // App_Images/Group_Display_Images/{id}.{type}
                _groupService.CreateGroup(new Group
                {
                    OrganizerId = User.Identity.GetUserId(),
                    City = model.City,
                    State = model.State,
                    GroupPictureUrl = model.GroupPictureUrl,
                    Name = model.Name,
                    Public = model.Public,
                    GroupInfo = model.GroupInfo
                },
                    model.GroupPicture, Server);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Groups/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Groups/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Groups/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Groups/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
