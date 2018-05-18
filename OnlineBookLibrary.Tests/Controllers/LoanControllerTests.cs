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
            loanController = new LoanController(loanMock.Object);
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
    }
}
