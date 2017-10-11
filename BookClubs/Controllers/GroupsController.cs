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
        IGroupService _groupService;
        IFileManager _fileManager;

        private static readonly string _defaultGroupPicPath = ConfigurationManager.AppSettings["DefaultGroupPicLocation"];

        public GroupsController(IGroupService groupService, IFileManager fileManager)
        {
            _groupService = groupService;
            _fileManager = fileManager;
        }

        // GET: Groups
        [AllowAnonymous]
        public ActionResult Index()
        {
            // old index query, query - won't include groups with no events yet - need to make it simpler
            //var viewModel = _groupService.GetAll().SelectMany(group => group.GroupEvents.OrderBy(ge => ge.DateTime)
            //                                                .Take(1), (group, nextEvent) =>
            //                                                new GroupListItemViewModel
            //                                                {
            //                                                    Id = group.Id,
            //                                                    GroupName = group.Name,
            //                                                    GroupCity = group.City,
            //                                                    GroupState = group.State,
            //                                                    CurrentBookTitle = nextEvent.Book.Title,
            //                                                    MemberCount = group.Users.Count().ToString()
            //                                                });

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
            //var group = _dataRepository.GetGroup(id);
            var group = _groupService.GetGroup(id);
            var currentUserId = User.Identity.GetUserId();

            var memberProfiles = group.Users.Select(u => new ProfileListViewModel()
            {
                Id = u.Id,
                Biography = u.Biography,
                FirstName = u.FirstName,
                LastName = u.LastName,
                ProfilePictureUrl = u.ProfilePictureUrl
            })
                .ToList();

            var wallPosts = group.GroupWallPosts.Select(gwp => new GroupWallPostListViewModel()
            {
                Id = gwp.Id,
                PosterId = gwp.PosterId,
                Body = gwp.Body,
                DateTime = gwp.TimeStamp.ToShortTimeString(),
                PosterName = gwp.Poster.FirstName + ", " + gwp.Poster.LastName
            })
                .ToList();

            var groupEvents = group.GroupEvents.Select(ge => new GroupEventListViewModel()
            {
                Id = ge.Id,
                BookName = ge.Book.Title,
                DateTime = ge.DateTime.ToLongTimeString(),
                Location = ge.City + ", " + ge.State
            })
                .OrderBy(ge => ge.DateTime)
                .ToList();

            // Determine if the user is a member of the specified group.
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
                // TODO: Add update logic here

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
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
