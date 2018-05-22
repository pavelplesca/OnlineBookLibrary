using Moq;
using NUnit.Framework;
using OnlineBookLibrary.Controllers;
using OnlineBookLibrary.Models;
using OnlineBookLibrary.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OnlineBookLibrary.Tests.Controllers
{
    [TestFixture]
    class LoanControllerTests
    {
        private LoanController loanController;
        private Mock<ILoanRepository> loanMock;

        [SetUp]
        public void Initialize()
        {
            loanMock = new Mock<ILoanRepository>();
            loanController = new LoanController(loanMock.Object);
        }

        [Test]
        public void Index_Returns_RedirectToRouteResult()
        {
            RedirectToRouteResult redirect = loanController.Index() as RedirectToRouteResult;

            Assert.That(redirect, Is.Not.Null);
        }

        [Test]
        public void DisplayHistory_Returns_PartialViewResult()
        {
            var coll = new List<Loan>
            {
                new Loan
                {
                    ID = 1
                }
            };

            loanMock.Setup(x => x.ReturnLoanHistory("1")).Returns(coll);
            var result = loanController.DisplayHistory("1");

            Assert.That(result, Is.TypeOf(typeof(PartialViewResult)));
        }

        [Test]
        public void DisplayActiveLoan_Returns_PartialViewResult_If_ActiveLoan_Is_Empty()
        {
            loanMock.Setup(x => x.ReturnActiveLoan("1")).Returns<Loan>(null);

            var result = loanController.DisplayActiveLoan("1") as PartialViewResult;

            Assert.That(result, Is.TypeOf(typeof(PartialViewResult)));
        }

        [Test]
        public void DisplayActiveLoan_Returns_PartialViewResult_If_ActiveLoan_Is_Not_Empty()
        {
            loanMock.Setup(x => x.ReturnActiveLoan("1")).Returns(new Loan());

            var result = loanController.DisplayActiveLoan("1") as PartialViewResult;

            Assert.That(result, Is.TypeOf(typeof(PartialViewResult)));
        }

        [Test]
        public void CheckIfUserRentsBook_Returns_PartialViewResult_When_User_Doesnt_Have_An_Active_Loan_Book_Available()
        {
            loanMock.Setup(x => x.UserHasActiveRent("1")).Returns(false);
            Book book = new Book { Status = BookStatus.Available };

            var result = loanController.CheckIfUserRentsBook("1", book) as PartialViewResult;

            Assert.That(result, Is.TypeOf<PartialViewResult>());
        }

        [Test]
        public void CheckIfUserRentsBook_Returns_EmptyResult_When_User_Doesnt_Have_An_Active_Loan_Book_Rented()
        {
            loanMock.Setup(x => x.UserHasActiveRent("1")).Returns(false);
            Book book = new Book { Status = BookStatus.Rented };

            var result = loanController.CheckIfUserRentsBook("1", book) as EmptyResult;

            Assert.That(result, Is.TypeOf<EmptyResult>());
        }

        [Test]
        public void CheckIfUserRentsBook_Returns_EmptyResult_When_User_Has_An_Active_Loan_Book_Rented()
        {
            loanMock.Setup(x => x.UserHasActiveRent("1")).Returns(true);
            Book book = new Book { Status = BookStatus.Rented };

            var result = loanController.CheckIfUserRentsBook("1", book) as EmptyResult;

            Assert.That(result, Is.TypeOf<EmptyResult>());
        }

        [Test]
        public void CheckIfUserRentsBook_Returns_EmptyResult_When_User_Has_An_Active_Loan_Book_Available()
        {
            loanMock.Setup(x => x.UserHasActiveRent("1")).Returns(true);
            Book book = new Book { Status = BookStatus.Available };

            var result = loanController.CheckIfUserRentsBook("1", book) as EmptyResult;

            Assert.That(result, Is.TypeOf<EmptyResult>());
        }

        [Test]
        public void CheckCurrentLoan_Returns_PartialViewResult_When_User_Has_Active_Rent_and_CurrentLoan_Violated()
        {
            loanMock.Setup(x => x.UserHasActiveRent("1")).Returns(true);
            loanMock.Setup(z => z.IsCurrentLoanViolated("1")).Returns(true);

            var result = loanController.CheckCurrentLoan("1") as PartialViewResult;

            Assert.That(result, Is.TypeOf<PartialViewResult>());
        }

        [Test]
        public void CheckCurrentLoan_Returns_EmptyResult_When_User_Has_Active_Rent_and_CurrentLoan_Not_Violated()
        {
            loanMock.Setup(x => x.UserHasActiveRent("1")).Returns(true);
            loanMock.Setup(z => z.IsCurrentLoanViolated("1")).Returns(false);

            var result = loanController.CheckCurrentLoan("1") as EmptyResult;

            Assert.That(result, Is.TypeOf<EmptyResult>());
        }

        [Test]
        public void CheckCurrentLoan_Returns_EmptyResult_When_User_Has_No_Active_Rent_and_CurrentLoan_Not_Violated()
        {
            loanMock.Setup(x => x.UserHasActiveRent("1")).Returns(false);
            loanMock.Setup(z => z.IsCurrentLoanViolated("1")).Returns(false);

            var result = loanController.CheckCurrentLoan("1") as EmptyResult;

            Assert.That(result, Is.TypeOf<EmptyResult>());
        }
    }
}
