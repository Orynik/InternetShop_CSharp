using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class CartController : Controller
    {
        private Cart cart = new Cart();
        private IBookRepository repository;

        public CartController(IBookRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index(string returnURL)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnURL,
            });
        }

        //Класс, возвращающий из сессионного хранилища состояние корзины
        public Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];

            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }

            return cart;
        }

        public RedirectToRouteResult AddToCart(int Id, string returnUrl)
        {
            Book book = repository.Books
                        .FirstOrDefault(b => b.Id == Id);
            if(book != null)
            {
                /*
                   т.к GetCart() возвращает Cart, то мы можем сразу вызвать метод AddItem
                   и добавить новую книгу.
                */
                GetCart().AddItem(book,1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(int bookId, string returnUrl)
        {
            Book book = repository.Books
                        .FirstOrDefault(b => b.Id == bookId);
            if (book != null)
            {
                /*
                   т.к GetCart() возвращает Cart, то мы можем сразу вызвать метод AddItem
                   и добавить новую книгу.
                */
                GetCart().RemoveItem(book);
            }

            return RedirectToAction("Index", new { returnUrl });
        }
    }
}