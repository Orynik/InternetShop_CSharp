using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Domain;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebUI.Controllers;
using WebUI.Models;
using WebUI.HtmlHelpers;

namespace UnitTes
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginated()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();

            mock.Setup(m => m.Books).Returns(new List<Book>
            {
                new Book{Id = 1, Name = "Book1"},
                new Book{Id = 2, Name = "Book2"},
                new Book{Id = 3, Name = "Book3"},
                new Book{Id = 4, Name = "Book4"},
                new Book{Id = 5, Name = "Book5"}
            });

            HomeController controller = new HomeController(mock.Object);

            controller.PageSize = 3;

            BooksListViewModel result = (BooksListViewModel)controller.List(2).Model;

            List<Book> Books = result.Books.ToList();

            Assert.IsTrue(Books.Count == 2);
            Assert.AreEqual(Books[0].Name, "Book4");
            Assert.AreEqual(Books[1].Name, "Book5");
        }
        [TestMethod]
        public void Can_Generate_Pagination_Links()
        {
            HtmlHelper myHelper = null;
            PagingInfo pagingInfo = new PagingInfo {
                CurrentPage = 3,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            Func<int, string> pageUrlDelegate = i => "Page" + i;

            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                            + @"<a class=""btn btn-default"" href=""Page2"">2</a>"
                            + @"<a class=""btn btn-pimary selected"" href=""Page3"">3</a>",
                            result.ToString());
        }
        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();

            mock.Setup(m => m.Books).Returns(new List<Book>
            {
                new Book{Id = 1, Name = "Book1"},
                new Book{Id = 2, Name = "Book2"},
                new Book{Id = 3, Name = "Book3"},
                new Book{Id = 4, Name = "Book4"},
                new Book{Id = 5, Name = "Book5"}
            });

            HomeController controller = new HomeController(mock.Object);

            controller.PageSize = 3;

            BooksListViewModel result = (BooksListViewModel)controller.List(2).Model;

            PagingInfo pagingInfo = result.PagingInfo;

            Assert.AreEqual(pagingInfo.CurrentPage, 2);
            Assert.AreEqual(pagingInfo.TotalItems, 5);
            Assert.AreEqual(pagingInfo.ItemsPerPage, 3);

        }
    }
}
