using BookClubs.Controllers;
using BookClubs.Data;
using BookClubs.Helpers;
using BookClubs.Models.ViewModels;
using Moq;
using NUnit.Framework;
using System.Configuration;
using System.IO;
using System.Web;

namespace BookClubs.Tests
{
    [TestFixture]
    public class AccountControllerTests
    {
        //[Test]
        //public void POST_EditProfile_Delete_Is_Called_When_Old_Profile_Pic_Is_Not_Default()
        //{
        //    //Arrange
        //    var repository = new Mock<IDataRepository>();
        //    var fileManager = new Mock<IFileManager>();
        //    var controller = new AccountController(repository.Object, fileManager.Object);
        //    var postedFile = new Mock<HttpPostedFileBase>();
        //    //var defaultPicturePath = ConfigurationManager.AppSettings["DefaultProfilePicLocation"];

        //    postedFile.SetupAllProperties();


        //    EditProfileViewModel viewModel = new EditProfileViewModel
        //    {
        //        ProfilePicture = postedFile.Object,
        //        ProfilePictureUrl = 
        //    }

        //    //Act

        //    //Assert
        //    fileManager.Verify(x => x.DeleteFile()
        //}

        //    [Test]
        //    public void POST_EditProfile_Delete_Is_Not_Called_When_Profile_Pic_Is_Default()
        //    {
        //        //Arrange
        //        var repository = new Mock<IDataRepository>();
        //        var fileManager = new Mock<IFileManager>();
        //        var controller = new AccountController(repository.Object, fileManager.Object);
        //        var postedFile = new Mock<HttpPostedFileBase>();
        //        var defaultPicturePath = ConfigurationManager.AppSettings["DefaultProfilePicLocation"];

        //        postedFile.SetupAllProperties();


        //        EditProfileViewModel viewModel = new EditProfileViewModel
        //        {
        //            ProfilePicture = postedFile.Object,
        //            ProfilePictureUrl =
        //        }

        //        //Act

        //        //Assert
        //        fileManager.Verify(x => x.DeleteFile()
        //    }
        //}
    }
}

