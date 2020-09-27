using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class AdminController : Controller
    {
         IBookRepository repository;

        public AdminController(IBookRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Books);
        }

        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Book book)
        {
            repository.CreateBook(book);
            TempData["message"] = string.Format("Книга \"{0}\" была добавлена в базу.", book.Name);
            return RedirectToAction("Index");
        }


        public ViewResult Delete(int Id)
        {
            Book book = repository.Books.FirstOrDefault(b => b.Id == Id);
            return View(book);
        }

        [HttpPost]
        public ActionResult Delete(Book book)
        {
            repository.DeleteBook(book);
            TempData["message"] = string.Format("Книга \"{0}\" удалена из базы.", book.Name);
            return RedirectToAction("Index");
        }

        public ViewResult Edit(int Id)
        {
            Book book = repository.Books.FirstOrDefault(b => b.Id == Id);
            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                repository.SaveBook(book);
                TempData["message"] = string.Format("Изменение данных о книге \"{0}\" сохранены", book.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(book);
            }
        }
    }
}