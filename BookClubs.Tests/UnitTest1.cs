using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookClubs.Data;

namespace BookClubs.Tests
{
    [TestClass]
    public class DevTests
    {
        [TestMethod]
        public void Test_Update_ApplicationUser_LINQ_Query()
        {
            EfDataRepository repo = new EfDataRepository();

            repo.UpdateProfile(new Models.User
            {
                FirstName = "Ian",
                LastName = "Gesner",
                Biography = "I'm pretty decent. Hire me.",
                GroupsIn = null
            });

        }
    }
}

