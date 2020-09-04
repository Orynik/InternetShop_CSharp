using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IBookRepository repository;
        public HomeController(IBookRepository repo)
        {
            repository = repo;
        }

        public ViewResult List()
        {
            return View(repository.Books);
        }
    }
}