using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cart
    {
        //Переменная для доступа к lineCollection
        public IEnumerable<CartItem> Lines { get { return lineCollection; } }

        List<CartItem> lineCollection = new List<CartItem>();

        //Добавление книги 
        public void AddItem(Book book, int quantity)
        {
            CartItem item = lineCollection
                            .Where(b => b.book.Id == book.Id)
                            .FirstOrDefault();
            if (item == null)
            {
                lineCollection.Add(new CartItem { book = book, Quantity = quantity });
            }
            else
            {
                item.Quantity += quantity;
            }
        }
        //Удаление книги
        public void RemoveItem(Book book)
        {
            lineCollection.RemoveAll(l => l.book.Id == book.Id);
        }

        //Вычисление общей стоймости
        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.book.Price * e.Quantity);
        } 

        //Очистка корзины
        public void Clear()
        {
            lineCollection.Clear();
        }
    }

    public class CartItem
    {
        public Book book;
        public int Quantity;
    }
}
