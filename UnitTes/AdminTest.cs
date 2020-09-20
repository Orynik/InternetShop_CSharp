using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Domain;
using Domain.Entities;
using System.Linq;
using WebUI.Controllers;
using System.Web.Mvc;

namespace UnitTes
{
    [TestClass]
    public class AdminTest
    {
        [TestMethod]
        public void Index_Contains_All_Books()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();

            Book book1 = new Book { Id = 1, Name = "book1" };
            Book book2 = new Book { Id = 2, Name = "book2" };
            Book book3 = new Book { Id = 3, Name = "book3" };

            mock.Setup(m => m.Books).Returns(new List<Book>
            {
                book1,
                book2,
                book3,
            });

            AdminController controller = new AdminController(mock.Object);

            List<Book> result = ((IEnumerable<Book>)controller.Index().ViewData.Model).ToList();

            Assert.IsTrue(result.Count() == 3);
            Assert.AreEqual(book2, result[1]);
        }
        [TestMethod]
        public void Can_Edit_Book()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();

            mock.Setup(m => m.Books).Returns(new List<Book>
            {
                new Book { Id = 1, Name = "book1" },
                new Book { Id = 2, Name = "book2" },
                new Book { Id = 3, Name = "book3" },
            });
            AdminController controller = new AdminController(mock.Object);

            Book book1 = controller.Edit(1).ViewData.Model as Book;
            Book book2 = controller.Edit(2).ViewData.Model as Book;
            Book book3 = controller.Edit(3).ViewData.Model as Book;

            Assert.AreEqual(1,book1.Id);
            Assert.AreEqual(2, book2.Id);
            Assert.AreEqual(3, book3.Id);
        }
        [TestMethod]
        public void Cannot_Edit_Nonexisten_Book()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();

            mock.Setup(m => m.Books).Returns(new List<Book>
            {
                new Book { Id = 1, Name = "book1" },
                new Book { Id = 2, Name = "book2" },
                new Book { Id = 3, Name = "book3" },
            });
            AdminController controller = new AdminController(mock.Object);

            Book result= controller.Edit(7).ViewData.Model as Book;

            Assert.IsNull(result);
        }
        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            AdminController controller = new AdminController(mock.Object);

            Book book = new Book {Name = "book1" };

            ActionResult result = controller.Edit(book);

            mock.Verify(m => m.SaveBook(book));

            Assert.IsNotInstanceOfType(result,typeof(ViewResult));
        }
        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            AdminController controller = new AdminController(mock.Object);

            Book book = new Book { Name = "book1" };

            controller.ModelState.AddModelError("error", "error");

            ActionResult result = controller.Edit(book);

            mock.Verify(m => m.SaveBook(It.IsAny<Book>()),Times.Never());

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
