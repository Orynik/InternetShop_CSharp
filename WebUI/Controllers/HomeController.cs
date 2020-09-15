using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IBookRepository repository;
        public int pageSize = 4;
        public HomeController(IBookRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(string genre, int page = 1)
        {
            BooksListViewModel model = new BooksListViewModel
            {
                Books = repository.Books
                 .Where(b => genre == null || b.Genre.TrimEnd(' ') == genre.TrimEnd(' '))
                 .OrderBy(book => book.Id)
                 .Skip((page - 1) * pageSize)
                 .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = 
                        genre == null ? 
                            repository.Books.Count() :
                            repository.Books.Where(b => b.Genre.TrimEnd(' ') == genre.TrimEnd(' ')).Count()
                },
                CurrentGenre = genre,
            };

            foreach(var book in model.Books)
            {
                var q = book;
            };

            return View(model);
        }
    }
}