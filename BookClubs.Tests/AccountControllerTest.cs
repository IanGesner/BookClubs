using BookClubs.Controllers;
using BookClubs.Data;
using BookClubs.Models.ViewModels;
using Moq;
using NUnit.Framework;
using System.IO;
using System.Web;

namespace BookClubs.Tests
{
    [TestFixture]
    public class AccountControllerTests
    {
        [Test]
        public void POST_EditProfile_Delete_Is_Called_When_Old_Profile_Pic_Is_User_Uploaded()
        {
            //Arrange
            var repository = new Mock<IDataRepository>();
            var controller = new AccountController(repository.Object);
            var postedFile = new Mock<HttpPostedFileBase>();

            postedFile.SetupAllProperties();

            EditProfileViewModel viewModel = new EditProfileViewModel
            {
                ProfilePicture = postedFile.Object,
                ProfilePictureUrl = 
            }
            
            //Act

            //Assert
            Assert.That()
        }
    }
}

