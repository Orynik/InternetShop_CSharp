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

        public ViewResult Index(Cart cart, string returnURL)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnURL,
            });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public RedirectToRouteResult AddToCart(Cart cart,int Id, string returnUrl)
        {
            Book book = repository.Books
                        .FirstOrDefault(b => b.Id == Id);
            if (book != null)
            {
                /*
                   т.к GetCart() возвращает Cart, то мы можем сразу вызвать метод AddItem
                   и добавить новую книгу.
                */
                cart.AddItem(book,1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int Id, string returnUrl)
        {
            Book book = repository.Books
                        .FirstOrDefault(b => b.Id == Id);
            if (book != null)
            {
                /*
                   т.к GetCart() возвращает Cart, то мы можем сразу вызвать метод AddItem
                   и добавить новую книгу.
                */
                cart.RemoveItem(book);
            }

            return RedirectToAction("Index", new { returnUrl });
        }
    }

    //Класс, возвращающий из сессионного хранилища состояние корзины

    //Legacy Code
    //Из-за создание связки с CartModelBinders
    /* public Cart GetCart()
    {
        Cart cart = (Cart)Session["Cart"];

        if (cart == null)
        {
            cart = new Cart();
            Session["Cart"] = cart;
        }

        return cart;
    }*/
}