using MailSender.Model;
using System.Net.Mail;

namespace MailSender.Interfaces
{
    public interface ISender
    {
        void Validate();
        void Send(MailMessage message);
        void Send(SenderInfo senderInfo, MailMessage message);
    }
}
