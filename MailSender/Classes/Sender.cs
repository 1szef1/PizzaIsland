using MailSender.Interfaces;
using MailSender.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using MailSender.Extensions;
using MailSender.Services;

namespace MailSender.Classes
{
    public class Sender : ISender
    {
        private SenderInfo senderInfo;

        public Sender()
        { }

        public void Send(MailMessage message)
        {
            senderInfo = GetConfiguration();
            SendMessage(message);
        }

        public void Send(SenderInfo senderInfo, MailMessage message)
        {
            this.senderInfo = senderInfo;
            SendMessage(message);
        }

        private void SendMessage(MailMessage message)
        {
            Validate();

            if (message.From == null)
                message.From = new MailAddress(senderInfo.FromEmail, senderInfo.DisplayName);

            var client = ConfigureClient();
            client.Send(message);
        }

        public void Validate()
        {
            if (senderInfo == null)
                throw new Exception("Brak zapisanych ustawień skrzynki nadawczej.");

            if (string.IsNullOrEmpty(senderInfo.Smtp))
                throw new Exception("Nie podano ustawień smtp.");

            if (string.IsNullOrEmpty(senderInfo.User))
                throw new Exception("Nie podano użytkownika.");

            if (string.IsNullOrEmpty(senderInfo.Password))
                throw new Exception("Nie ustawiono hasła.");

            if (senderInfo.Port <= 0)
                throw new Exception("Nieprawidłowy port.");
        }

        private SenderInfo GetConfiguration()
        {
            return new SenderService().LoadConfiguration();
        }

        private SmtpClient ConfigureClient()
        {
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(senderInfo.User, senderInfo.Password.Decode());
            client.Host = senderInfo.Smtp;
            client.Port = senderInfo.Port;
            client.EnableSsl = senderInfo.SecureConnection;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            return client;
        }
    }
}
