﻿using BookClubs.Data;
using BookClubs.Models;
using BookClubs.Models.ViewModels;
using BookClubs.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookClubs.Controllers
{
    public class GroupsController : Controller
    {
        //IDataRepository _dataRepository;
        IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        // GET: Groups
        public ActionResult Index()
        {
            // Name, City, State - Books - Members
            //var viewModel = _dataRepository.GetAllGroups().SelectMany(group => group.GroupEvents.OrderBy(ge => ge.DateTime)
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

            var viewModel = _groupService.GetAll().SelectMany(group => group.GroupEvents.OrderBy(ge => ge.DateTime)
                                                            .Take(1), (group, nextEvent) =>
                                                            new GroupListItemViewModel
                                                            {
                                                                Id = group.Id,
                                                                GroupName = group.Name,
                                                                GroupCity = group.City,
                                                                GroupState = group.State,
                                                                CurrentBookTitle = nextEvent.Book.Title,
                                                                MemberCount = group.Users.Count().ToString()
                                                            });

            return View(viewModel);
        }

        // GET: Groups/Details/5
        public ActionResult Details(int id)
        {
            //var group = _dataRepository.GetGroup(id);
            var group = _groupService.GetGroup(id);

            var memberProfiles = group.Users.Select(u => new ProfileListViewModel()
            {
                Id = u.Id,
                Biography = u.Biography,
                FirstName = u.FirstName,
                LastName = u.LastName,
                ProfilePictureUrl = u.ProfilePictureUrl
            }).ToList();

            if (group != null)
            {
                var viewModel = new GroupDetailsViewModel
                {
                    Id = group.Id,
                    GroupName = group.Name,
                    GroupState = group.State,
                    GroupCity = group.City,
                    CurrentBookTitle = group.GroupEvents.FirstOrDefault().Book.Title,
                    MemberCount = group.Users.Count.ToString(),
                    MemberProfiles = memberProfiles,
                    ProfilePictureUrl = group.GroupPictureUrl
                };

                return View(viewModel);
            }
            else
                return new HttpNotFoundResult("We couldn't find the group you requested.");
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
