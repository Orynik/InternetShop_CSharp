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

            controller.pageSize = 3;

            BooksListViewModel result = (BooksListViewModel)controller.List(null,2).Model;

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
                            + @"<a class=""btn btn-primary selected"" href=""Page3"">3</a>",
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

            controller.pageSize = 3;

            BooksListViewModel result = (BooksListViewModel)controller.List(null,2).Model;

            PagingInfo pagingInfo = result.PagingInfo;

            Assert.AreEqual(pagingInfo.CurrentPage, 2);
            Assert.AreEqual(pagingInfo.TotalItems, 5);
            Assert.AreEqual(pagingInfo.ItemsPerPage, 3);

        }
        [TestMethod]
        public void Can_Filter_Books()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();

            mock.Setup(m => m.Books).Returns(new List<Book>
            {
                new Book{Id = 1, Name = "Book1",Genre = "Genre1"},
                new Book{Id = 2, Name = "Book2",Genre = "Genre2"},
                new Book{Id = 3, Name = "Book3",Genre = "Genre1"},
                new Book{Id = 4, Name = "Book4",Genre = "Genre2"},
                new Book{Id = 5, Name = "Book5",Genre = "Genre1"}
            });

            HomeController controller = new HomeController(mock.Object);

            controller.pageSize = 3;

            List<Book> result = ((BooksListViewModel)controller.List("Genre1",1).Model).Books.ToList();

            Assert.AreEqual(result.Count(), 3);
            Assert.IsTrue(result[0].Id == 1 && result[0].Name == "Book1" && result[0].Genre == "Genre1");
            Assert.IsTrue(result[1].Id == 3 && result[1].Name == "Book3" && result[1].Genre == "Genre1");
            Assert.IsTrue(result[2].Id == 5 && result[2].Name == "Book5" && result[2].Genre == "Genre1");
        }
        [TestMethod]
        public void Can_Create_Categories()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();

            mock.Setup(m => m.Books).Returns(new List<Book>
            {
                new Book{Id = 1, Name = "Book1",Genre = "Genre1"},
                new Book{Id = 2, Name = "Book2",Genre = "Genre2"},
                new Book{Id = 3, Name = "Book3",Genre = "Genre3"},
                new Book{Id = 4, Name = "Book4",Genre = "Genre2"},
                new Book{Id = 5, Name = "Book5",Genre = "Genre1"}
            });

            NavController target = new NavController(mock.Object);

            List<string> result = ((IEnumerable<string>)target.Menu().Model).ToList();

            Assert.AreEqual(result.Count(), 3);
            Assert.AreEqual(result[0], "Genre1");
            Assert.AreEqual(result[1], "Genre2");
            Assert.AreEqual(result[2], "Genre3");
        }

        [TestMethod]
        public void Can_View_CurrentGenre()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();

            mock.Setup(m => m.Books).Returns(new List<Book>
            {
                new Book{Id = 1, Name = "Book1",Genre = "Genre1"},
                new Book{Id = 2, Name = "Book2",Genre = "Genre2"},
                new Book{Id = 3, Name = "Book3",Genre = "Genre3"},
                new Book{Id = 4, Name = "Book4",Genre = "Genre2"},
                new Book{Id = 5, Name = "Book5",Genre = "Genre1"}
            });

            NavController target = new NavController(mock.Object);

            string currentGenre = "Genre1";

            string result = target.Menu(currentGenre).ViewBag.SelectedGenre;

            Assert.AreEqual(currentGenre, result);
        }

        [TestMethod]
        public void Generate_Gener_Specific()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();

            mock.Setup(m => m.Books).Returns(new List<Book>
            {
                new Book{Id = 1, Name = "Book1",Genre = "Genre1"},
                new Book{Id = 2, Name = "Book2",Genre = "Genre2"},
                new Book{Id = 3, Name = "Book3",Genre = "Genre3"},
                new Book{Id = 4, Name = "Book4",Genre = "Genre2"},
                new Book{Id = 5, Name = "Book5",Genre = "Genre1"}
            });

            HomeController controller = new HomeController(mock.Object);
            controller.pageSize = 3;



            int res1 = ((BooksListViewModel)controller.List("Genre1").Model).PagingInfo.TotalItems;
            int res2 = ((BooksListViewModel)controller.List("Genre2").Model).PagingInfo.TotalItems;
            int res3 = ((BooksListViewModel)controller.List("Genre3").Model).PagingInfo.TotalItems;
            int all = ((BooksListViewModel)controller.List(null).Model).PagingInfo.TotalItems;

            Assert.AreEqual(2, res1);
            Assert.AreEqual(2, res2);
            Assert.AreEqual(1, res3);
            Assert.AreEqual(5, all);

        }
    }
}
