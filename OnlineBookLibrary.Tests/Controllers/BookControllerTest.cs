using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineBookLibrary.Controllers;
using OnlineBookLibrary.Models;
using OnlineBookLibrary.Persistence.Repositories;

namespace OnlineBookLibrary.Tests.Controllers
{
    [TestClass]
    public class BookControllerTest
    {
        private BookController controller;
        //private Mock<IBookRepository> repositoryMock;

        //[TestInitialize]
        //public void SetupContext()
        //{
        //    repositoryMock new Mock<IBookRepository>();
        //    controller = new BookController();
        //}

        
        [TestMethod]
        public void IndexViewResultNotNull()
        {
            // Arrange
            var mock = new Mock<IBookRepository>();
            controller = new BookController(mock.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void IndexViewBagPageIs1()
        {
            // Arrange
            var mock = new Mock<IBookRepository>();
            controller = new BookController(mock.Object);
            var expected = 1 ;

            // Act
            ViewResult result = controller.Index() as ViewResult;
            var actual =   result.ViewBag.Page;

            // Assert
            Assert.AreEqual(actual,expected);
        }

        [TestMethod]
        public void BookDetailsRedirectIfIdIsNull()
        {
            // Arrange
            var mock = new Mock<IBookRepository>();
            controller = new BookController(mock.Object);

            // Act
            var result = controller.BookDetails(null);
            
            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void BookDetailsRedirectIfNoBookFound()
        {
            // Arrange
            var mock = new Mock<IBookRepository>();
            mock.Setup(a => a.GetBookDetailsById(5)).Returns((Book) null);
            controller = new BookController(mock.Object);

            // Act
            var result = controller.BookDetails(5);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void BookDetails()
        {
            // Arrange
            var mock = new Mock<IBookRepository>();
            mock.Setup(a => a.GetBookDetailsById(5)).Returns(new Book());
            controller = new BookController(mock.Object);

            // Act
            var result = controller.BookDetails(5);
            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result,typeof(ViewResult));
            var model = (result as ViewResult).Model;
            Assert.IsInstanceOfType(model, typeof(Book));
        }


    }
}
