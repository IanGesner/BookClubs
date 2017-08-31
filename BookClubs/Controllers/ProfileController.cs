using BookClubs.Data;
using BookClubs.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookClubs.Controllers
{
    public class ProfileController : Controller
    {
        IDataRepository _repo;

        public ProfileController(IDataRepository repo)
        {
            _repo = repo;
        }

        //GET: Profile
        //[HttpGet]
        //public new ActionResult View(string username)
        //{
        //    var user = _repo.GetApplicationUserByUsername(username);

        //    ProfileGetViewModel model = new ProfileGetViewModel()
        //    {
        //        Biography = user.Biography,
        //        FirstName = user.FirstName,
        //        LastName = user.LastName,
        //        ProfilePictureUrl = 
        //    }
        //    return View();
        //}
    }
}