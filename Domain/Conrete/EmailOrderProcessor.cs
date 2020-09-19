using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Conrete
{ 
    public class EmailSetting 
    {
        public string MailToAddress = "orders@example.com";
        public string MailFromAddress = "bookstore@example.com";
        public bool UseSsl = true;
        public string Username = "MySmtpNicknname";
        public string Password = "MySmtlPassword";
        public string ServerName = "smtp.example.com";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = @"c:\book_store_emails";
    }
    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSetting emailSetting;

        public EmailOrderProcessor(EmailSetting emailSetting)
        {
            this.emailSetting = emailSetting;
        }

        public void ProcessOrder(Cart cart, ShopingDetails shopingDetails)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSetting.UseSsl;
                smtpClient.Host = emailSetting.ServerName;
                smtpClient.Port = emailSetting.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSetting.Username, emailSetting.Password);

                if (emailSetting.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSetting.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .AppendLine("Новый заказ обработан")
                    .AppendLine("----")
                    .AppendLine("Товары:");

                foreach (var line in cart.Lines)
                {
                    var subtotal = line.book.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (Итого: {2:c})", line.Quantity, line.book.Name, subtotal);
                }

                body.AppendFormat("Общая стоймость: {0:c}", cart.ComputeTotalValue())
                    .AppendLine("------")
                    .AppendLine("Доставка:")
                    .AppendLine(shopingDetails.Name)
                    .AppendLine(shopingDetails.line1)
                    .AppendLine(shopingDetails.line2 ?? "")
                    .AppendLine(shopingDetails.line3 ?? "")
                    .AppendLine(shopingDetails.City)
                    .AppendLine(shopingDetails.Country)
                    .AppendLine("------")
                    .AppendFormat("Подарочная упоковка: {0}", shopingDetails.WrapGift ? "Да" : "Нет");

                MailMessage mailMessage = new MailMessage(
                    emailSetting.MailFromAddress,
                    emailSetting.MailToAddress,
                    "Новый заказ отправлен!",
                    body.ToString()
                );

                if (emailSetting.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.UTF8;
                }

                smtpClient.Send(mailMessage);
            }
        }
    }
}
