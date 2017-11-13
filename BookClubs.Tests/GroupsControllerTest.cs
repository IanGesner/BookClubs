using BookClubs.Controllers;
using BookClubs.Data.Repositories;
using BookClubs.Helpers;
using BookClubs.Models;
using BookClubs.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace BookClubs.Tests
{
    //[TestFixture]
    //public class GET_Index
    //{
    //    [Test]
    //    public void service_getall_should_be_called()
    //    {
    //        // ****************************** ARRANGE ******************************
    //        //var mockRepository = new Mock<IGroupRepository>();
    //        //mockRepository.Get
    //        var mockGroupService = new Mock<IGroupService>();
    //        mockGroupService.SetupAllProperties();


    //        var groups = new List<Group>()
    //        {
    //            new Group()
    //        }.AsQueryable();

    //        mockGroupService.Setup(x => x.GetAll()).Returns(groups);

    //        // SUT
    //        var groupsController = new GroupsController(mockGroupService.Object, It.IsAny<IFileManager>());

    //        // ****************************** ACT ******************************
    //        groupsController.Index();

    //        // ****************************** ASSERT ******************************
    //        mockGroupService.VerifyAll();
    //    }

    //}
}
