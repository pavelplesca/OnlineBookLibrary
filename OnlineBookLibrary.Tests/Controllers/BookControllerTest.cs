using System;
using System.Collections.Generic;
using System.Linq;
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
        private Mock<IBookRepository> repoMock;
        
        [TestInitialize]
        public void Init()
        {
            repoMock = new Mock<IBookRepository>();
        }
        

        [TestMethod]
        public void Index_ViewResult_Not_Null()
        {
            // Arrange
            controller = new BookController(repoMock.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void Index_ViewBagPage_Is_1()
        {
            // Arrange
            controller = new BookController(repoMock.Object);
            var expected = 1 ;

            // Act
            ViewResult result = controller.Index() as ViewResult;
            var actual =   result.ViewBag.Page;

            // Assert
            Assert.AreEqual(actual,expected);
        }

        [TestMethod]
        public void BookDetails_Redirect_If_Id_Is_Null()
        {
            // Arrange
            controller = new BookController(repoMock.Object);

            // Act
            var result = controller.BookDetails(null);
            
            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void BookDetails_Redirects_If_No_Book_Found()
        {
            // Arrange
            repoMock.Setup(a => a.GetBookDetailsById(5)).Returns((Book) null);
            controller = new BookController(repoMock.Object);

            // Act
            var result = controller.BookDetails(5);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void BookDetails_Returns_View_For_Book()
        {
            // Arrange
            repoMock.Setup(a => a.GetBookDetailsById(5)).Returns(new Book());
            controller = new BookController(repoMock.Object);

            // Act
            var result = controller.BookDetails(5);
            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result,typeof(ViewResult));
            var model = (result as ViewResult).Model;
            Assert.IsInstanceOfType(model, typeof(Book));
        }

        [TestMethod]
        public void TopLoans_Returns_Empty_If_No_Loans_For_Month()
        {
            // Arrange
            repoMock.Setup(a => a.GetTopLoanedBooks(3)).Returns(new List<Book>());
            controller = new BookController(repoMock.Object);

            // Act
            var result = controller.TopLoans();

            // Assert
            Assert.IsInstanceOfType(result, typeof(EmptyResult));
        }

        [TestMethod]
        public void TopLoans_Returns_Partial_If_Two_Book_Has_Loans()
        {
            // Arrange
            repoMock.Setup(a => a.GetTopLoanedBooks(3)).Returns(new List<Book>(){ new Book(), new Book()});
            controller = new BookController(repoMock.Object);

            // Act
            var result = controller.TopLoans();

            // Assert
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
            var books = (IEnumerable<Book>)(result as PartialViewResult).Model;
            Assert.AreEqual(2,books.Count());
        }

        [TestMethod]
        public void ReturnTags_Returns_EmptyResult_If_No_Tags_In_Repository()
        {
            // Arrange
            repoMock.Setup(a => a.GetTags()).Returns(new List<Tag>());
            controller = new BookController(repoMock.Object);

            // Act
            var result = controller.ReturnTags();

            // Assert
            Assert.IsInstanceOfType(result,typeof(EmptyResult));
        }

        [TestMethod]
        public void ReturnTags_Returns_Partial_With_3_Tags_If_3_Tags_In_Repository()
        {
            repoMock.Setup(a => a.GetTags()).Returns(new List<Tag>(){ new Tag(), new Tag(), new Tag()});
            controller = new BookController(repoMock.Object);

            // Act
            var result = controller.ReturnTags();

            // Assert
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
            var model = (IEnumerable<Tag>)((result as PartialViewResult).Model);
            Assert.AreEqual(3,model.Count());
        }
    }
}
