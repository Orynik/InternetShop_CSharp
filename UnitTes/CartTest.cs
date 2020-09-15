using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Entities;
using System.Linq;

namespace UnitTes
{
    [TestClass]
    public class CartTest
    {
        [TestMethod]
        public void Can_Add_New_Item()
        {
            Book book1 = new Book { Id = 1, Name = "book1" };
            Book book2 = new Book { Id = 2, Name = "book2" };
            Book book3 = new Book { Id = 3, Name = "book3" };

            Cart cart = new Cart();

            cart.AddItem(book1, 1);
            cart.AddItem(book2, 1);
            cart.AddItem(book3, 1);

            List<CartItem> result = cart.Lines.ToList();

            Assert.AreEqual(3, result.Count());
            Assert.AreEqual(book1, result[0].book);
            Assert.AreEqual(book2, result[1].book);
            Assert.AreEqual(book3, result[2].book);
        }

        [TestMethod]
        public void Can_Add_Quantity()
        {
            Book book1 = new Book { Id = 1, Name = "book1" };
            Book book2 = new Book { Id = 1, Name = "book1" };
            Book book3 = new Book { Id = 3, Name = "book3" };

            Cart cart = new Cart();

            cart.AddItem(book1, 1);
            cart.AddItem(book2, 1);
            cart.AddItem(book3, 1);

            List<CartItem> result = cart.Lines.ToList();

            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(2, result[0].Quantity);
            Assert.AreEqual(1, result[1].Quantity);
        }

        [TestMethod]
        public void Can_Remove_All()
        {
            Book book1 = new Book { Id = 1, Name = "book1" };
            Book book2 = new Book { Id = 1, Name = "book1" };
            Book book3 = new Book { Id = 3, Name = "book3" };

            Cart cart = new Cart();

            cart.AddItem(book1, 1);
            cart.AddItem(book2, 1);
            cart.AddItem(book3, 1);

            cart.Clear();

            List<CartItem> result = cart.Lines.ToList();

            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void Can_Remove_Item()
        {
            Book book1 = new Book { Id = 1, Name = "book1" };
            Book book2 = new Book { Id = 2, Name = "book2" };
            Book book3 = new Book { Id = 3, Name = "book3" };

            Cart cart = new Cart();

            cart.AddItem(book1, 1);
            cart.AddItem(book2, 1);
            cart.AddItem(book3, 1);

            cart.RemoveItem(book1);

            List<CartItem> result = cart.Lines.ToList();

            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(book3, result[1].book);
        }

        [TestMethod]
        public void Can_Compute_Total_Count()
        {
            Book book1 = new Book { Id = 1, Name = "book1", Price = 500 };
            Book book2 = new Book { Id = 1, Name = "book1", Price = 500 };
            Book book3 = new Book { Id = 3, Name = "book3", Price = 500 };

            Cart cart = new Cart();

            cart.AddItem(book1, 1);
            cart.AddItem(book2, 1);
            cart.AddItem(book3, 1);

            decimal result = cart.ComputeTotalValue();

            Assert.AreEqual(Convert.ToDecimal(1500), result);
        }
    }
}
