using MailSender.Model;
using PizzaIsland.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaIsland.Interfaces
{
    interface IResender
    {
        void SendOrderMessage(OrderHeader order);
        void SendTestMessage(SenderInfo info, string email);
        void SendToken(string email, string token);
    }
}
