using MeanTime.Controllers;
using MeanTime.Data;
using MeanTime.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;

namespace MeanTimeTests
{
    [TestClass]
    public class AppsControllerTests
    {
        private ApplicationDbContext _context;
        private AppsController controller;

        [TestInitialize]
        public void TestInitialize()
        {
            // set up the in-memory db that's needed in every test
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);

            // Creating a Test Genre
            var genre = new Genre
            {
                GenreId = 100,
                Type = "Test Genre",
                MetaDescription = "Test Generation MetaDescription ...",
                Logo = null
            };

            // Adding the test genre to the mock database
            _context.Add(genre);

            // Adding apps to the database
            for (var i=0; i < 3; i++)
            {
                var app = new App
                {
                    AppId = i+1,
                    Name = "App-" + i,
                    Image = "Image-" + i,
                    Price = 10 + i,
                    Size = 100 + i,
                    MetaTag = "App Meta Tag-" + i,
                    Rating = i,
                    Status = true,
                    GenreId = 100
                };
                _context.Add(app);

            }

            // Adding app details to the database
            for (var i=0; i < 3; i++)
            {
                var appDetail = new AppDetail
                {
                    AppId=i+1,
                    Owner = "Owner_App-" + i,
                    Mode = "Online/Offline" + i,
                    Description = "This is a sample app Description for app - " + i,
                    Duration = 100 + i,
                    TotalDataUsage = 1000 + i,
                    AvgMemoryUsage = 100 + i,
                    Downloads = (i + 1) + "M",
                    InstallDate = DateTime.Now
                };
                _context.Add(appDetail);
            }

            // Saving the changes made
            _context.SaveChanges();

            controller = new AppsController(_context);
        }


        #region "Delete"


        // Tests for the Delete() - Delete() GET method
        #region "Delete() - GET"
        [TestMethod]
        public void DeleteWithNullIdReturns404()
        {
            // Arrange
            // Moved to TestInitialize()

            // Act
            var result = (ViewResult)controller.Delete(null).Result;


            // Assert
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DeleteWithNoAppsinDatabaseReturns404()
        {
            // Clearing any app data in the mock database 
            _context.Apps = null;
            _context.SaveChanges();

            //Act
            var result = (ViewResult)controller.Delete(1).Result;

            //Assert 
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DeleteWithInvalidIdReturns404()
        {
            //act 
            var result = (ViewResult)controller.Delete(-1).Result;

            //assert
            Assert.AreEqual("404", result.ViewName);

        }

        [TestMethod]
        public void DeleteWithValidIdReturnsDeleteView()
        {
            //act 
            var result = (ViewResult)controller.Delete(1).Result;

            //assert
            Assert.AreEqual("Delete", result.ViewName);
        }

        [TestMethod]
        public void DeleteWithValidIdReturnsAppModel()
        {
            //act 
            var result = (ViewResult)controller.Delete(1).Result;

            //assert - comparing the models being returned
            Assert.AreEqual(result.Model, _context.Apps.Find(1));
        }

        #endregion

        // Tests for DeleteConfirmed() - Delete() POST method
        #region "DeleteConfirmed() - POST"

        // I had to change the return statement for this one!!
        [TestMethod]
        public void DeleteConfirmedWithNoAppsInDatabaseReturnsErrorView()
        {
            // clearing any records for the Apps Table
            _context.Apps = null;
            _context.SaveChanges();

            //act
            var result = (ViewResult)controller.DeleteConfirmed(1).Result;

            //assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void DeleteConfirmedWithValidIdRedirectsToActionIndex()
        {
            //act 
            var result = (RedirectToActionResult)controller.DeleteConfirmed(1).Result;

            //assert
            Assert.AreEqual("Index", result.ActionName);
        }

        [TestMethod]
        public void DeleteConfirmedWithValidIdRemovesApp()
        {
            // Excluding the app with appId = 1 from the list, for comparison later
            var appsList = _context.Apps.Where(a => a.AppId != 1).ToList();

            //act
            var result = controller.DeleteConfirmed(1).Result;

            //assert
            CollectionAssert.AreEqual(appsList, _context.Apps.ToList());
        }

        #endregion

        
        #endregion
    }
}