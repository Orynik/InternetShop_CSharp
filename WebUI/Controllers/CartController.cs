using Domain;
using Domain.Abstract;
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
        private IOrderProcessor orderProcessor;

        public CartController(IBookRepository repo, IOrderProcessor processor)
        {
            repository = repo;
            orderProcessor = processor;
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

        public RedirectToRouteResult AddToCart(Cart cart, int Id, string returnUrl)
        {
            Book book = repository.Books
                        .FirstOrDefault(b => b.Id == Id);
            if (book != null)
            {
                cart.AddItem(book, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int Id, string returnUrl)
        {
            Book book = repository.Books
                        .FirstOrDefault(b => b.Id == Id);
            if (book != null)
            {
                cart.RemoveItem(book);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public ViewResult CheckOut()
        {
            return View(new ShopingDetails());
        }

        [HttpPost]
        public ViewResult CheckOut(Cart cart, ShopingDetails shopingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Извините, корзина пуста!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shopingDetails);
                cart.Clear();
                return View("Complited");
            }
            else
            {
                return View(new ShopingDetails());
            }
        }
    }
}