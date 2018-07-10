using MailSender.Classes;
using MailSender.Interfaces;
using MailSender.Model;
using PizzaIsland.Data.Model;
using PizzaIsland.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PizzaIsland.Classes
{
    class Resender : IResender
    {
        public void SendOrderMessage(OrderHeader order)
        {
            if (!string.IsNullOrEmpty(order.Email))
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(new MailAddress(order.Email));
                mail.Subject = $"Zamówienie z {App.Name}";
                mail.IsBodyHtml = true;

                StringBuilder body = new StringBuilder();
                body.Append("" +
                    "<head>" +
                    "<style>" +
                        "table {font-family: arial, sans-serif; border-collapse: collapse; width: 50%;} " +
                        "td, th {border: 1px solid #dddddd; text-align: left; padding: 8px;} " +
                        "tr:nth-child(even) {background-color: #F4F4F4;} " +
                    "</style>" +
                    "</head>");
                body.Append("<table>");
                body.Append("<tr><th>Produkt</th><th>Ilość</th><th>Cena</th></tr>");

                decimal sum = 0;
                foreach (var p in order.OrderItems)
                {
                    body.Append($"<tr><td>{p.Product.Name}</td><td>{p.Count}</td><td>{(p.Count * p.Price).ToString("C")}</td></tr>");
                    sum += p.Price * p.Count;
                }
                body.Append($"<tr><th>Razem</th><th></th><th>{sum.ToString("C")}</th></tr></table>");
                body.Append($"<br><b>Komentarz</b><br>{order.Comments}");
                mail.Body = body.ToString();

                ISender mailSender = new Sender();
                mailSender.Send(mail);
            }
        }

        public void SendTestMessage(SenderInfo info, string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(new MailAddress(email));
                mail.Subject = $"Wiadomość testowa z {App.Name}";
                mail.Body = "Test skrzynki nadawczej pomyślny";

                ISender mailSender = new Sender();
                mailSender.Send(info, mail);
            }
        }

        public void SendToken(string email, string token)
        {
            if (!string.IsNullOrEmpty(email))
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(new MailAddress(email));
                mail.Subject = $"Wiadomość z {App.Name}";
                mail.Body = "Token: " + token;

                ISender mailSender = new Sender();
                mailSender.Send(mail);
            }
        }
    }
}
